using DAL.Entitties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using System.Data.Entity.Infrastructure;

namespace _3_Layer_EntityFramework
{
    public partial class ccbKhoa : Form
    {
        private readonly StudentBUS studentBUS = new StudentBUS();
        private readonly KhoaBUS khoaBUS = new KhoaBUS();
        private readonly MajorBUS majorBUS = new MajorBUS();
        public bool isEdit = false;

        public ccbKhoa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            List<Khoa> khoas = khoaBUS.GetAllKhoa();
            FillFacultyCombobox(khoas);
        }

        private void LoadData()
        {
            List<SinhVien> sinhViens = studentBUS.GetAll();
            BindGrid(sinhViens);
        }

        private void ClearInput()
        {
            txtMSSV.Clear();
            txtTen.Clear();
            txtDiemTB.Clear();
            cbbKhoa.SelectedIndex = -1;
            
        }

        private void BindGrid(List<SinhVien> sinhViens)
        {
            dgvStudent.Rows.Clear();
            foreach (var s in sinhViens)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = s.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = s.FullName;
                dgvStudent.Rows[index].Cells[2].Value = s.Khoa?.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = s.AverageScore;
                dgvStudent.Rows[index].Cells[4].Value = s.Major != null ? s.Major.Name : "Chưa có chuyên ngành";
            }
        }

        private void FillFacultyCombobox(List<Khoa> listFacultys)
        {
            cbbKhoa.DataSource = listFacultys;
            cbbKhoa.DisplayMember = "FacultyName";
            cbbKhoa.ValueMember = "FacultyID";
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            try
            {
                
                string studentID = txtMSSV.Text;

                var existingSinhVien = studentBUS.FindByID(studentID);

                if (existingSinhVien == null) 
                {
                    SinhVien newSinhVien = new SinhVien
                    {
                        StudentID = studentID,
                        FullName = txtTen.Text,
                        AverageScore = float.Parse(txtDiemTB.Text),
                        FacultyID = (int)cbbKhoa.SelectedValue,
                    };
                    studentBUS.AddSinhVien(newSinhVien);
                    MessageBox.Show("Thêm sinh viên thành công!");
                }
                else 
                {
                    existingSinhVien.FullName = txtTen.Text;
                    existingSinhVien.AverageScore = float.Parse(txtDiemTB.Text);
                    existingSinhVien.FacultyID = (int)cbbKhoa.SelectedValue;
                    studentBUS.UpdateSinhVien(existingSinhVien);
                    MessageBox.Show("Cập nhật sinh viên thành công!");
                }

                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }



        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    string selectedStudentID = dgvStudent.SelectedRows[0].Cells[0].Value.ToString();
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        studentBUS.DeleteSinhVien(selectedStudentID);
                        LoadData();
                        ClearInput();
                        MessageBox.Show("Xóa sinh viên thành công!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];

                txtMSSV.Text = selectedRow.Cells[0].Value.ToString();
                txtTen.Text = selectedRow.Cells[1].Value.ToString();
                txtDiemTB.Text = selectedRow.Cells[3].Value.ToString();

                var selectedFacultyName = selectedRow.Cells[2].Value.ToString();
                cbbKhoa.SelectedIndex = cbbKhoa.FindStringExact(selectedFacultyName);

                
                
            }
        }

        private void cbCheck_CheckedChanged(object sender, EventArgs e)
        {
            List<SinhVien> sinhViens = studentBUS.GetAll();

            if (cbCheck.Checked)
            {
                sinhViens = sinhViens.Where(sv => sv.Major == null).ToList();
            }
            BindGrid(sinhViens);
        }
    }
}
