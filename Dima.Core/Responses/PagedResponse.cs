using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dima.Core.Responses
{
    //TipoGenérico, pq ele vai servir tanto para listar categorias, transações etc.
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = Configuration.DefaultPageNumber, int pageSize = Configuration.DefaultPageSize) : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = Configuration.DefaultPageNumber;
            PageSize = pageSize;
        }

        public PagedResponse(TData? data, int code = Configuration.DefaultStatusCode, string? message = null) : base(data, code, message)
        {

        }


        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}

//Comentários para meu controle pessoal
//Classe que possui Resposta no FrontEnd as requisições dos endpoints. No caso aqui, se refere a paginação no método GetAll
