using System;
using System.Collections.Generic;
using System.Linq;
namespace QuanLySinhVien
{
    public class diemso
    {
        public string masv { get; set; }
        public string mamon { get; set; }
        public int sotc { get; set; }
        public double diem { get; set; }

        public diemso(string masv, string mamon, int sotc, double diem)
        {
            this.masv = masv;
            this.mamon = mamon;
            this.sotc = sotc;
            this.diem = diem;
        }
    }
    public class sinhvien
    {
        public string masv { get; set; }
        public string tensv { get; set; }
        public double diemtb { get; set; }

        public sinhvien(string masv, string tensv, double diemtb)
        {
            this.masv = masv;
            this.tensv = tensv;
            this.diemtb = 0;
        }
        public void tinhdiemtb(List<diemso> dsdiem)
        {
            List<diemso> diemcuasv = dsdiem.Where(d => d.masv == masv).ToList();

            if (diemcuasv.Count == 0)
            {
                diemtb = 0;
                return;
            }
            double tongDiemTichLuy = 0;
            int tongSoTinChi = 0;
            foreach (var d in diemcuasv)
            {
                tongDiemTichLuy += d.diem * d.sotc;
                tongSoTinChi += d.sotc;
            }
            diemtb = tongSoTinChi > 0 ? Math.Round(tongDiemTichLuy / tongSoTinChi, 2) : 0;
        }
        public string XepLoai()
        {
            if (diemtb >= 9.0) return "Xuất sắc";
            if (diemtb >= 8.0) return "Giỏi";
            if (diemtb >= 6.5) return "Khá";
            if (diemtb >= 5.0) return "Trung bình";
            return "Yếu / Kém";
        }
        public void HienThiThongTin()
        {
            Console.WriteLine($"| {masv,-6} | {tensv,-20} | ĐTB: {diemtb,5:F2} | Xếp loại: {XepLoai(),-10} |");
        }
    }
        class Program
        {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Tạo danh sách sinh viên sử dụng List<SinhVien>
            List<sinhvien> dssinhvien = new List<sinhvien>
            {
                new sinhvien("SV01", "Nguyễn Văn An", 0),
                new sinhvien("SV02", "Trần Thị Bích",0 ),
                new sinhvien("SV03", "Lê Hoàng Cường",0)
            };

            // 2. Tạo danh sách điểm số sử dụng List<DiemSo>
            List<diemso> dsDiem = new List<diemso>
            {
                // Điểm môn học của SV01
                new diemso("SV01", "CS101", 3, 9.0),
                new diemso("SV01", "CS102", 4, 8.5),

                // Điểm môn học của SV02
                new diemso("SV02", "CS101", 3, 7.0),
                new diemso("SV02", "CS102", 4, 6.0),

                // Điểm môn học của SV03
                new diemso("SV03", "CS101", 3, 4.0),
                new diemso("SV03", "CS102", 4, 4.5)
            };

            // 3. Tính điểm trung bình cho từng sinh viên
            foreach (var sv in dssinhvien)
            {
                sv.tinhdiemtb(dsDiem);
            }

            // 4. Hiển thị danh sách tổng quan
            Console.WriteLine("=================== DANH SÁCH SINH VIÊN ===================");
            foreach (var sv in dssinhvien)
            {
                sv.HienThiThongTin();
            }

            // 5. Phân biệt sinh viên theo danh mục xếp loại (Grouping)
            Console.WriteLine("\n================ PHÂN LOẠI KẾT QUẢ HỌC TẬP ================");
            var groups = dssinhvien.GroupBy(sv => sv.XepLoai());

            foreach (var group in groups)
            {
                Console.WriteLine($"\n[Nhóm xếp loại: {group.Key}]");
                foreach (var sv in group)
                {
                    Console.WriteLine($" -> {sv.tensv} ({sv.masv}) - Điểm trung bình: {sv.diemtb}");
                }
            }
        }
    }
}