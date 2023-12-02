using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WindowsFormsApp1
{
    public partial class frmPhong : Form
    {
        public frmPhong()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet("dsKYTUCXA");
        SqlDataAdapter daPhong;
        SqlDataAdapter daLoaiPhong;
        SqlDataAdapter daTinhTrangPhong;

        private void frmPhong_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=KYTUCXA;Integrated Security=True";
            conn.Open();

            string sQueryLoaiPhong = @"SELECT * FROM LOAIPHONG";
            daLoaiPhong = new SqlDataAdapter(sQueryLoaiPhong, conn);
            daLoaiPhong.Fill(ds, "tblLoaiPhong");
            cboLoaiPhong.DataSource = ds.Tables["tblLoaiPhong"];
            cboLoaiPhong.DisplayMember = "LOAIPHONG";
            cboLoaiPhong.ValueMember = "MALOAIPHONG";

            string sQueryTinhTrangPhong = @"SELECT * FROM  TINHTRANGPHONG";
            daTinhTrangPhong = new SqlDataAdapter(sQueryTinhTrangPhong, conn);
            daTinhTrangPhong.Fill(ds, "tblTinhTrangPhong");
            cboTinhTrang.DataSource = ds.Tables["tblTinhTrangPhong"];
            cboTinhTrang.DisplayMember = "TINHTRANGPHONG";
            cboTinhTrang.ValueMember = "MATINHTRANGPHONG";

            string sQueryPhong = @"SELECT p.*, lp.LOAIPHONG, ttp.TINHTRANGPHONG
                    FROM PHONG p, LOAIPHONG lp, TINHTRANGPHONG ttp
                    WHERE p.MALOAIPHONG = lp.MALOAIPHONG AND p.MATINHTRANGPHONG = ttp.MATINHTRANGPHONG ";
            daPhong = new SqlDataAdapter(sQueryPhong, conn);

            daPhong.Fill(ds, "tblDSPHONG");
            dgDSPhong.DataSource = ds.Tables["tblDSPHONG"];
            dgDSPhong.Columns["MAPHONG"].HeaderText = "Mã phòng";
            dgDSPhong.Columns["SOPHONG"].HeaderText = "Số phòng";
            dgDSPhong.Columns["LOAIPHONG"].HeaderText = "Loại phòng";
            dgDSPhong.Columns["TANG"].HeaderText = "Tầng";
            dgDSPhong.Columns["SOLUONGSINHVIEN"].HeaderText = "Số lượng sinh viên";
            dgDSPhong.Columns["TINHTRANGPHONG"].HeaderText = "Tình trạng phòng";
            dgDSPhong.Columns["TIENPHONG"].HeaderText = "Tiền phòng";


            dgDSPhong.Columns["MALOAIPHONG"].Visible = false;
            dgDSPhong.Columns["MATINHTRANGPHONG"].Visible = false;

            string sThemPhong = @"INSERT INTO PHONG VALUES(@MAPHONG, @SOPHONG, @MALOAIPHONG, @TANG, @SOLUONGSINHVIEN, @MATINHTRANGPHONG, @TIENPHONG)";
            SqlCommand cmThemPhong = new SqlCommand(sThemPhong, conn);
            cmThemPhong.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");
            cmThemPhong.Parameters.Add("@SOPHONG", SqlDbType.VarChar, 10, "SOPHONG");
            cmThemPhong.Parameters.Add("@MALOAIPHONG", SqlDbType.VarChar, 10, "MALOAIPHONG");
            cmThemPhong.Parameters.Add("@TANG", SqlDbType.NVarChar, 21, "TANG");
            cmThemPhong.Parameters.Add("@SOLUONGSINHVIEN", SqlDbType.Int, 8, "SOLUONGSINHVIEN");
            cmThemPhong.Parameters.Add("@MATINHTRANGPHONG", SqlDbType.VarChar, 10, "MATINHTRANGPHONG");
            cmThemPhong.Parameters.Add("@TIENPHONG", SqlDbType.Money, 21, "TIENPHONG");


            daPhong.InsertCommand = cmThemPhong;

            string sSuaPhong = @"UPDATE PHONG SET SOPHONG = @SOPHONG, MALOAIPHONG = @MALOAIPHONG, TANG = @TANG, SOLUONGSINHVIEN = @SOLUONGSINHVIEN, MATINHTRANGPHONG = @MATINHTRANGPHONG, TIENPHONG = @TIENPHONG WHERE MAPHONG = @MAPHONG";
            SqlCommand cmSuaPhong = new SqlCommand(sSuaPhong, conn);
            cmSuaPhong.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");
            cmSuaPhong.Parameters.Add("@SOPHONG", SqlDbType.VarChar, 10, "SOPHONG");
            cmSuaPhong.Parameters.Add("@MALOAIPHONG", SqlDbType.VarChar, 10, "MALOAIPHONG");
            cmSuaPhong.Parameters.Add("@TANG", SqlDbType.NVarChar, 21, "TANG");
            cmSuaPhong.Parameters.Add("@SOLUONGSINHVIEN", SqlDbType.Int, 8, "SOLUONGSINHVIEN");
            cmSuaPhong.Parameters.Add("@MATINHTRANGPHONG", SqlDbType.VarChar, 10, "MATINHTRANGPHONG");
            cmSuaPhong.Parameters.Add("@TIENPHONG", SqlDbType.Money, 21, "TIENPHONG");


            daPhong.UpdateCommand = cmSuaPhong;

            string sXoaPhong = "DELETE FROM PHONG WHERE MAPHONG = @MAPHONG";
            SqlCommand cmXoaPhong = new SqlCommand(sXoaPhong, conn);
            cmXoaPhong.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");

            daPhong.DeleteCommand = cmXoaPhong;

            
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;



        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daPhong.Update(ds, "tblDSPHONG");
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgDSPhong.Refresh();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["tblDSPHONG"].NewRow();
            row["MAPHONG"] = txtMaPhong.Text;
            row["SOPHONG"] = txtSoPhong.Text;
            row["MALOAIPHONG"] = cboLoaiPhong.SelectedValue;
            row["LOAIPHONG"] = cboLoaiPhong.Text;
            row["TANG"] = txtTang.Text;
            row["SOLUONGSINHVIEN"] = txtSoLuongSV.Text;

            row["MATINHTRANGPHONG"] = cboTinhTrang.SelectedValue;
            row["TINHTRANGPHONG"] = cboTinhTrang.Text;

            row["TIENPHONG"] = txtTienPhong.Text;


            ds.Tables["tblDSPHONG"].Rows.Add(row);

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSPhong.SelectedRows[0];

            // Begin editing the row
            dgDSPhong.BeginEdit(true);

            // Update the row with the new values
            dr.Cells["MAPHONG"].Value = txtMaPhong.Text;
            dr.Cells["SOPHONG"].Value = txtSoPhong.Text;
            dr.Cells["MALOAIPHONG"].Value = cboLoaiPhong.SelectedValue;
            dr.Cells["TANG"].Value = txtTang.Text;
            dr.Cells["SOLUONGSINHVIEN"].Value = txtSoLuongSV.Text;
            dr.Cells["MATINHTRANGPHONG"].Value = cboTinhTrang.SelectedValue;

            dr.Cells["TIENPHONG"].Value = txtTienPhong.Text;

            // End editing the row
            dgDSPhong.EndEdit();

            // Display a message to indicate successful edit
            MessageBox.Show("Đã sửa", "Thông báo");

            // Enable "Lưu" and "Hủy" buttons
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

           
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblDSPHONG"].RejectChanges();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSPhong.SelectedRows[0];
            dgDSPhong.Rows.Remove(dr);

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Close();
        }

        private void dgDSPhong_Click(object sender, EventArgs e)
        {
            if (dgDSPhong.SelectedRows.Count > 0)
            {
                DataGridViewRow dr = dgDSPhong.SelectedRows[0];
                txtMaPhong.Text = dr.Cells["MAPHONG"].Value.ToString();
                txtSoPhong.Text = dr.Cells["SOPHONG"].Value.ToString();
                cboLoaiPhong.SelectedValue = dr.Cells["MALOAIPHONG"].Value.ToString();
                txtTang.Text = dr.Cells["TANG"].Value.ToString();
                txtSoLuongSV.Text = dr.Cells["SOLUONGSINHVIEN"].Value.ToString();
                cboTinhTrang.SelectedValue = dr.Cells["MATINHTRANGPHONG"].Value.ToString();
                txtTienPhong.Text = dr.Cells["TIENPHONG"].Value.ToString();

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoMaPhong.Checked)
            {
                string maPhongCanTim = txtTimKiem.Text;

                // Sử dụng DataView để lọc dữ liệu từ DataSet
                DataView dv = new DataView(ds.Tables["tblDSPHONG"]);
                dv.RowFilter = $"MAPHONG = '{maPhongCanTim}'";

                if (dv.Count > 0)
                {
                    dgDSPhong.DataSource = dv.ToTable();
                }

                if (txtTimKiem.Text == "")
                {
                    dgDSPhong.DataSource = ds.Tables["tblDSPHONG"];
                }
            }

            if (rdoSoPhong.Checked)
            {
                string tenPhongCanTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSPHONG"]);

                if (!string.IsNullOrEmpty(tenPhongCanTim))
                {
                    dv.RowFilter = $"SOPHONG LIKE '%{tenPhongCanTim}%'";
                }

                dgDSPhong.DataSource = dv.ToTable();

                if (txtTimKiem.Text == "")
                {
                    dgDSPhong.DataSource = ds.Tables["tblDSPHONG"];
                }
            }
        }

    }
}

