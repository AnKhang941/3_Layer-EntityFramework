using DAL.Entitties;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class StudentBUS
    {
        QLSV context = new QLSV();
        public List<SinhVien> GetAll()
        {
            
            return context.SinhViens.ToList();
        }
        public void AddSinhVien(SinhVien sinhVien)
        {
            context.SinhViens.Add(sinhVien);
            context.SaveChanges();
        }

        
        public void UpdateSinhVien(SinhVien updatedSinhVien)
        {
            var existingSinhVien = context.SinhViens.Find(updatedSinhVien.StudentID);
            if (existingSinhVien != null)
            {
                existingSinhVien.FullName = updatedSinhVien.FullName;
                existingSinhVien.AverageScore = updatedSinhVien.AverageScore;
                existingSinhVien.FacultyID = updatedSinhVien.FacultyID;
                existingSinhVien.MajorID = updatedSinhVien.MajorID;
                context.SaveChanges();
            }
        }

        public SinhVien FindByID(String studentID)
        {
            QLSV context = new QLSV();
            return context.SinhViens.FirstOrDefault(p=> p.StudentID == studentID);
        }
        public void DeleteSinhVien(string studentID)
        {
            var sinhVien = context.SinhViens.Find(studentID);
            if (sinhVien != null)
            {
                context.SinhViens.Remove(sinhVien);
                context.SaveChanges();
            }
        }

        public void InsertUpdate(SinhVien sinhVien, bool isEdit)
        {
            using (var context = new QLSV())
            {
                if (isEdit)
                {
                    // Cập nhật sinh viên
                    var existingStudent = context.SinhViens.Find(sinhVien.StudentID);
                    if (existingStudent != null)
                    {
                        context.Entry(existingStudent).CurrentValues.SetValues(sinhVien);
                    }
                }
                else
                {
                    // Thêm sinh viên mới
                    context.SinhViens.Add(sinhVien);
                }

                context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }


    }
}

    
    


