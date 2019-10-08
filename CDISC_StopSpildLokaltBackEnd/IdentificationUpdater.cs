using System;
using System.Linq;

namespace CDISC_StopSpildLokaltBackEnd {
    public class IdentificationUpdater {

        private static object lockObject = new object();

        private readonly OrganizationalDBContext _context;

        public IdentificationUpdater(OrganizationalDBContext context) {
            _context = context;
        }

        public void RefreshIdentifications() {
            lock(lockObject) {

                foreach (Volunteer v in _context.Volunteers) {

                    var identification = _context.Identifications.Where(i => i.Id == v.IdentificationId).Single();

                    if (v.VolunteerType.Equals(VolunteerType.PASSIVE)) identification.Active = false;
                    else identification.Active = true;

                    v.Identification.LastUpdatedTs = DateTime.Now;

                    _context.Update(identification);

                }
                _context.SaveChanges();
            }
        }
    }
}
