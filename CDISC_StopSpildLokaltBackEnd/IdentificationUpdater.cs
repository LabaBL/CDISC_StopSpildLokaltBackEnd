using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CDISC_StopSpildLokaltBackEnd {
    public class IdentificationUpdater {

        private static Object lockObject = new Object();

        private readonly OrganizationalDBContext _context;

        public IdentificationUpdater(OrganizationalDBContext context) {
            _context = context;
        }

        public void RefreshIdentifications() {
            lock(lockObject) {


                //TODO New Volunteers must have their Identification setup at creation
                //TODO Get all Volunteers

                var volunteers = _context.Volunteers;

                foreach (Volunteer v in volunteers) {

                    // Remove existing Identification
                    if (v.Identification != null) {
                        _context.Identifications.Remove(v.Identification);

                        v.IdentificationId = -1;
                        v.Identification = null;
                        _context.Update(v);
                    }

                    // If still active, create new Identification
                    if(v.VolunteerType.Equals(VolunteerType.VOLUNTEER) || v.VolunteerType.Equals(VolunteerType.CONTACT_PERSON) || v.VolunteerType.Equals(VolunteerType.TRIAL)) {

                        var identification = new Identification {
                            CreatedTs = DateTime.Now,
                            Volunteer = v,
                            UniqueToken = Guid.NewGuid()
                        };

                        _context.Add(identification);

                        v.Identification = identification;
                        v.IdentificationId = identification.Id;
                        _context.Update(v);
                    }

                    _context.SaveChanges();
                } //TODO Does this work?
            }
        }
    }
}
