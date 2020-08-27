using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SAT.MVC.DATA.EF
{


    #region Course

    public class CourseMetadata
    {
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(50, ErrorMessage = "Course Name must be 50 characters or less")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(50, ErrorMessage = "Course Name must be 50 characters or less")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
                
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [UIHint("MutlilineText")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "* Value must be a whole number.")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string CreditHours { get; set; }

        [Display(Name = "Virtual")]
        public Nullable<bool> IsVirtual { get; set; }

    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }

    #endregion

    #region Enrollment

    public class EnrollmentMetadata
    {

        [Display(Name = "Enrollment Date")]
        [DisplayFormat(NullDisplayText = "[-N/A-]", DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }

    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }

    #endregion

    #region ScheduledClass

    public class ScheduledClassMetadata
    {
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Instructor")]
        [StringLength(50, ErrorMessage = "* Instructor name must be 50 characters or less.")]
        public string InstructorName { get; set; }

        [Display(Name = "Class Building Location")]
        [StringLength(50, ErrorMessage = "* Location must be 50 characters or less.")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string Location { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass { }

    #endregion

    #region ScheduledClassStatus

    public class ScheduledClassStatusMetadata
    {

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [StringLength(50, ErrorMessage = "Class Status must be 50 characters or less")]
        [Display(Name = "Class Status")]
        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [UIHint("MutlilineText")]
        public string Description { get; set; }

        [Display(Name = "Full Class")]
        public bool IsFull { get; set; }

    }

    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus { }

    #endregion

    #region Student

    public class StudentMetadata
    {

        [Required(ErrorMessage = "* First Name is required")]
        [StringLength(25, ErrorMessage = "First Name must be 25 characters or less")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name must be 50 characters or less")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(NullDisplayText = "[-N/A-]", DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DOB { get; set; }
        
        [StringLength(25, ErrorMessage = "* Major must be 25 characters or less.")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string Major { get; set; }

        [Display(Name = "Scholarship")]
        public Nullable<bool> HasScholarship { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "* Email address is required")]
        [EmailAddress(ErrorMessage ="Invalid email adress")]
        public string Email { get; set; }

        [Display(Name = "Street Address")]
        [StringLength(50, ErrorMessage = "* Street Address must be 50 characters or less.")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "City must be 50 characters or less")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "State must be 2 characters", MinimumLength = 2)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string State { get; set; }

        [StringLength(6, ErrorMessage = "Zipcode must be between 5-6 characters", MinimumLength = 5)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        [Display(Name = "Zip Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "* Emergency Contact is required")]
        [StringLength(50, ErrorMessage = "Emergency Contact must be 50 characters or less")]
        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

        [Required(ErrorMessage = "* Emergency contact number is required")]
        [Display(Name = "Emergency Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string EmergencyPhone { get; set; }

        [Required(ErrorMessage = "* Phone number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string StudentPhone { get; set; }

        [Display(Name = "Student Photo")]
        public string StudentImage { get; set; }

    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        [Display(Name = "Student")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    #endregion

    #region StudentStatus

    public class StudentStatusMetadata
    {
        
        [Display(Name = "Student Status")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string StatusName { get; set; }

        [DisplayFormat(NullDisplayText = "[N/A]")]
        [UIHint("MutlilineText")]
        public string Description { get; set; }

    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }

    #endregion




}
