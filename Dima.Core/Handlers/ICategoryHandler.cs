using Dima.Core.Entities;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
    }
}

//Comentários para meu controle pessoal
//Interface que determina como os Handlers devem ser moldados ( uma tarefa assincrona, de resposta Categoria com os nomes dos metodos e recebendo os requests do Front como Parametro)
