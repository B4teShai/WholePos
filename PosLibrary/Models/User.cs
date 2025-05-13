using System;
using System.ComponentModel.DataAnnotations;

namespace PosLibrary.Models
{
    /// <summary>
    /// POS системд хэрэглэгчдийн үүсгэнэ.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Хэрэглэгчийн ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Хэрэглэгчийн нэр.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Хэрэглэгчийн нууц үг.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Хэрэглэгчийн эрхийг илэрхийлнэ.
        /// </summary>
        [Required]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Хэрэглэгч бүтээгдэхүүн өөрчилж чадах эсэхийг илэрхийлнэ.
        /// </summary>
        /// <returns>Үнэн бол True; Үгүй бол False.</returns>
        public abstract bool CanEditProducts();

        /// <summary>
        /// Хэрэглэгч бүтээгдэхүүн устгаж чадах эсэхийг илэрхийлнэ.
        /// </summary>
        /// <returns>Үнэн бол True; Үгүй бол False.</returns>
        public abstract bool CanDeleteProducts();

        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Менежер хэрэглэгч бүтээгдэхүүн үйлдэл хийх чадвартай.
    /// </summary>
    public class Manager : User
    {
        /// <summary>
        /// Менежер хэрэглэгч классын шинэ жишээг эхлүүлнэ.
        /// </summary>
        public Manager()
        {
            Role = "Manager";
        }

        /// <summary>
        /// Менежер бүтээгдэхүүн өөрчилж чадах эсэхийг илэрхийлнэ.
        /// </summary>
        /// <returns>Үнэн бол True; Үгүй бол False.</returns>
        public override bool CanEditProducts()
        {
            return true;
        }

        /// <summary>
        /// Менежер бүтээгдэхүүн устгаж чадах эсэхийг илэрхийлнэ.
        /// </summary>
        /// <returns>Үнэн бол True; Үгүй бол False.</returns>
        public override bool CanDeleteProducts()
        {
            return true;
        }
    }

    /// <summary>
    /// Кассир хэрэглэгч бүтээгдэхүүн үйлдэл хийх чадвартай.
    /// </summary>
    public class Cashier : User
    {
        /// <summary>
        /// Кассир хэрэглэгч классын шинэ жишээг эхлүүлнэ.
        /// </summary>
        public Cashier()
        {
            Role = "Cashier";
        }

        /// <summary>
        /// Кассир бүтээгдэхүүн өөрчилж чадах эсэхийг илэрхийлнэ.
        /// </summary>
        /// <returns>Үнэн бол True; Үгүй бол False.</returns>
        public override bool CanEditProducts()
        {
            return false;
        }

        /// <summary>
        /// Кассир бүтээгдэхүүн устгаж чадах эсэхийг илэрхийлнэ.
        /// </summary>
        /// <returns>Үнэн бол True; Үгүй бол False.</returns>
        public override bool CanDeleteProducts()
        {
            return false;
        }
    }
} 