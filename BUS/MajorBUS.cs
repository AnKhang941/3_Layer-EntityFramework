
using DAL.Entitties;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BUS
{
    public class MajorBUS
    {
        QLSV context = new QLSV();


        public List<Major> GetAllMajors()
        {
            return context.Majors.ToList();
        }

        
        public void AddMajor(Major major)
        {
            context.Majors.Add(major);
            context.SaveChanges();
        }

        
        public void UpdateMajor(Major updatedMajor)
        {
            var existingMajor = context.Majors.Find(updatedMajor.MajorID);
            if (existingMajor != null)
            {
                existingMajor.Name = updatedMajor.Name;
                context.SaveChanges();
            }
        }

        
        public void DeleteMajor(int majorID)
        {
            var major = context.Majors.Find(majorID);
            if (major != null)
            {
                context.Majors.Remove(major);
                context.SaveChanges();
            }
        }
    }
}
