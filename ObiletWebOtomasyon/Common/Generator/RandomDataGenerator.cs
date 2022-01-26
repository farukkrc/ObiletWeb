﻿using System;
using System.Text;

namespace ObiletWebOtomasyon.Common.Generator
{
    public static class RandomDataGenerator
    {
        public static string RandomMail(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString().ToLower();
        }

        public static string RandomPassword(int length)
        {
            const string valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
