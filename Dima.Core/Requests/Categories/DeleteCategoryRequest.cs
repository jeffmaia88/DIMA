using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        public long Id { get; set; }
    }
}

//Comentários para meu controle pessoal
//Classe que possui os parametros que será exibidos no FrontEnd no endpoint Delete