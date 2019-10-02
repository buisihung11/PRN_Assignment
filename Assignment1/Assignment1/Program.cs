using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment1
{

    class Student
    {
        private string mMaSV;

        public string MaSV
        {
            get { return mMaSV; }
            set { mMaSV = value; }
        }

        private string hoTen;

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        private DateTime ngaySinh;

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        private string diaChi;

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        private string dienThoai;

        public string DienThoai
        {
            get { return dienThoai; }
            set { dienThoai = value; }
        }

        public Student()
        {

        }

        public Student(string mMaSV, string hoTen, DateTime ngaySinh, string diaChi, string dienThoai)
        {
            this.mMaSV = mMaSV;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.diaChi = diaChi;
            this.dienThoai = dienThoai;
        }

        public void XemThongTin()
        {
            Console.WriteLine($"Ma SV: {MaSV}\tHo Ten: {HoTen}\tNgay Sinh: {NgaySinh}\tDia Chi: {DiaChi}\tSDT: {DienThoai}");
        }

    }

     static class GetInput
    {
        public static string GetString(string msg,string err)
        {
            string data = null;

            do
            {
                Console.Write(msg + ": ");
                data = Console.ReadLine();
                if(data.Trim().Length == 0)
                    Console.WriteLine(err);
            } while (data.Trim().Length == 0);

            return data;
        }

        public static DateTime GetDate(string msg,string err)
        {
            DateTime data = new DateTime();

            do
            {
                Console.Write(msg+ ": ");
                try
                {
                    data = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException f)
                {
                    Console.WriteLine(err);
                    continue;
                }
                return data;
            } while (true);

        }

        public static string GetPhoneNumber(string msg, string err)
        {
            Regex rx = new Regex("^\\d{10,11}$");
            string data = null;

            do
            {
                Console.Write(msg + ": ");
                try
                {
                    data = Console.ReadLine();
                    //MatchCollection matches = rx.Matches(data);
                    if (!rx.IsMatch(data))
                    {
                        Console.WriteLine(err);
                        continue;
                    }
                }
                catch (FormatException f)
                {
                    Console.WriteLine(err);
                    continue;
                }
                return data;
            } while (true);

            return data;
        }


    }



    class Program
    {
        const int MAX_NUMBER = 1;
        static List<Student> students = new List<Student>(MAX_NUMBER);
        static SortedDictionary<string, Student> studentMap = new SortedDictionary<string, Student>();
        public void Menu()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("=====Student Manager=====");
                Console.WriteLine("1. Xem danh sach sinh vien");
                Console.WriteLine("2. Them sinh vien");
                Console.WriteLine("3. Tim sinh vien");
                Console.WriteLine("4. Cap nhat thong tin sinh vien");
                Console.WriteLine("5. Exit");
                Console.Write("Nhap: ");
                choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: ShowAllStudentsMenu(); break;
                    case 2: AddStudentMenu();  break;
                    case 3: FindStudentMenu();  break;
                    case 4: UpdateStudentMenu();  break;
                    default: break;
                }

            } while (choice != 5);
        }

        #region MenuFunction
        private void AddStudentMenu()
        {
            string mMaSV, hoTen, diaChi, dienThoai;
            DateTime ngaySinh;

            do
            {
                mMaSV = GetInput.GetString("Nhap MaSV", "Vui long nhap lai");
                //check student was in List
                if (FindStudent(mMaSV) != null)
                {
                    Console.WriteLine("That student was exsits");
                    return;
                }
                ngaySinh = GetInput.GetDate("Nhap ngaysinh (MM/DD/YYYY)", "Vui long nhap lai");
                hoTen = GetInput.GetString("Nhap Ho Ten", "Vui long nhap lai");
                diaChi = GetInput.GetString("Nhap Dia CHi", "Vui long nhap lai");
                dienThoai = GetInput.GetPhoneNumber("Nhap so dt", "Nhap dung dinh dang");

                AddStudent(mMaSV, hoTen, ngaySinh, diaChi, dienThoai);
                Console.WriteLine("Them thanh cong");
                return;
            } while (true);


        }

        private void ShowAllStudentsMenu()
        {
            if(students.Count == 0)
                Console.WriteLine("No student in List");
            else
            {
                Console.WriteLine("List of Students: ");
                foreach (var student in students)
                    student.XemThongTin();
            }
            
        }

        private void FindStudentMenu()
        {
            if (students.Count == 0)
                Console.WriteLine("No student in List");
            else
            {
                string mMaSv = GetInput.GetString("Nhap MaSV Can tim: ", "Vui long nhap lai");
                Student s = FindStudent(mMaSv);
                if (s == null)
                {
                    Console.WriteLine("Khong tim thay sinh vien " + mMaSv);
                }
                else
                {
                    s.XemThongTin();
                }
            }
            
        }

        private void UpdateStudentMenu()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No student in List");
            }
            else
            {

                string hoTen, diaChi, dienThoai;
                DateTime ngaySinh;
                string mMaSv = GetInput.GetString("Nhap MaSV Can cap nhat: ", "Vui long nhap lai");

                Student s = FindStudent(mMaSv);
                if (s == null)
                {
                    Console.WriteLine("Khong tim thay sinh vien " + mMaSv);
                }
                else

                {
                    ngaySinh = GetInput.GetDate("Nhap ngaysinh (MM/DD/YYYY)", "Vui long nhap lai");
                    hoTen = GetInput.GetString("Nhap Ho Ten", "Vui long nhap lai");
                    diaChi = GetInput.GetString("Nhap Dia CHi", "Vui long nhap lai");
                    dienThoai = GetInput.GetPhoneNumber("Nhap so dt", "Nhap dung dinh dang");
                    UpdateStudent(mMaSv, hoTen, ngaySinh, diaChi, dienThoai);

                    Console.WriteLine("Cap nhat thanh cong");
                }
            }
        }

        #endregion

        #region MenuHelper
        private Student FindStudent(string mMaSV)
        {
            foreach (var student in students)
            {
                if (student.MaSV.ToLower().Equals(mMaSV.ToLower()))
                {
                    return student;
                }
            }
            return null;
        }

        private Student AddStudent(string mMaSV, string hoTen, DateTime ngaySinh, string diaChi, string dienThoai)
        {

            if (FindStudent(mMaSV) != null)
                return null;

            Student newStudent = new Student(mMaSV, hoTen, ngaySinh, diaChi, dienThoai);
            students.Add(newStudent);
            return newStudent;
        }

        private Student UpdateStudent(string mMaSV, string hoTen, DateTime ngaySinh, string diaChi, string dienThoai)
        {
            Student s = FindStudent(mMaSV);
            if (s == null)
                return null;
            s.HoTen = hoTen;
            s.NgaySinh = ngaySinh;
            s.DiaChi = diaChi;
            s.DienThoai = dienThoai;
            return s;
        }

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Menu();



            //Student s = new Student("SE1", "Hung Bui", DateTime.Parse("12/12/2000"), "Quan 9", "123123123");
            //s.XemThongTin();
            //Console.WriteLine("Enter birthdat(MM/dd/yyyy");
            //birthday = DateTime.Parse(Console.ReadLine());


        }
    }
}
