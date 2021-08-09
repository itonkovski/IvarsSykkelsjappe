namespace IvarsSykkelsjappe.Data
{
    public class DataConstants
    {
        public class Bike
        {
            public const int BrandMinLength = 3;
            public const int BrandMaxLength = 20;

            public const int ModelMinLength = 3;
            public const int ModelMaxLength = 20;

            public const int DescriptionMinLength = 10;
        }

        public class Category
        {
            public const int NameMaxLength = 20;
        }

        public class Product
        {
            public const int BrandMinLength = 3;
            public const int BrandMaxLength = 20;

            public const int ModelMinLength = 3;
            public const int ModelMaxLength = 20;

            public const int DescriptionMinLength = 10;

            public const string ProductNumberRegularExpression = @"[A-Z]{2}[0-9]{6}";
        }

        public class Service
        {
            public const int NameMaxLength = 20;
        }

        public class Booking
        {
            public const int FullNameMaxLength = 30;
            public const int PhoneNumberMaxLength = 30;
        }

        public class Mechanic
        {
            public const string EmployeeNumberRegularExpression = @"[0-9]{3}[A-Z]4";
            public const int FullNameMaxLength = 30;
            public const int PhoneNumberMaxLength = 30;
        }
    }
}
