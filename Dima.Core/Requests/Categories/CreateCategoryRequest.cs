﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class CreateCategoryRequest :Request
    {
        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(80, ErrorMessage = "Titulo deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição Inválida")]
        public string Description { get; set; } = string.Empty;

    }
}
