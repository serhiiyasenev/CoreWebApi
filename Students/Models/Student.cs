﻿namespace FirstWebApplication.Models
{
    public class Student
    {
        public int    Id      { get; set; }
        public string Name    { get; set; }
        public int    Score   { get; set; }
        public Teacher Teacher { get; set;}
    }
}