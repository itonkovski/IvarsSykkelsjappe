﻿using System;
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
    }
}
