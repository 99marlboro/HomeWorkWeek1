using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeWorkWeek1.Models
{
    public class 客戶資料登入ViewModel
    {
        [Required]
        [Display(Name = "帳號")]
        public string 帳號 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string 密碼 { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }
    }
}