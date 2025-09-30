using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests
{
    public abstract class Request
    {
        public string UserId { get; set; } = string.Empty;
    }
}

//Comentários para meu controle pessoal
//Classe base para qualquer request, elas herdam sempre esse UserId
