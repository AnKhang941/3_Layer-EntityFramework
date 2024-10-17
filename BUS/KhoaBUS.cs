
using DAL.Entitties;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BUS
{
    public class KhoaBUS
    {
        QLSV context = new QLSV();


        public List<Khoa> GetAllKhoa()
        {
            return context.Khoas.ToList();
        }

        
        public void AddKhoa(Khoa khoa)
        {
            context.Khoas.Add(khoa);
            context.SaveChanges();
        }

        
        public void UpdateKhoa(Khoa updatedKhoa)
        {
            var existingKhoa = context.Khoas.Find(updatedKhoa);
            if (existingKhoa != null)
            {
                existingKhoa.FacultyName = updatedKhoa.FacultyName;
                context.SaveChanges();
            }
        }

        
        public void DeleteKhoa(int khoaID)
        {
            var khoa = context.Khoas.Find(khoaID);
            if (khoa != null)
            {
                context.Khoas.Remove(khoa);
                context.SaveChanges();
            }
        }
    }
}

