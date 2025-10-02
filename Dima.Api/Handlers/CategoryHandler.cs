using Dima.Api.Data;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace Dima.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 201, "Categoria Criada com Sucesso");

            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao criar categoria");
            }



        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria deletada com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao deletar categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            
            try
            {
                var categories = await context.Categories.AsNoTracking()
                                                         .Where(c => c.UserId == request.UserId) // pode quebrar aqui tbm
                                                         .Skip((request.PageNumber - 1) * request.PageSize)
                                                         .Take(request.PageSize)
                                                         .ToListAsync();

                var count = await context.Categories
                                         .AsNoTracking()
                                         .Where (x => x.UserId == request.UserId)
                                         .OrderBy(x => x.Title)// pode dar erro aqui
                                         .CountAsync();

                return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>>(null, 500, "Erro ao buscar categorias");
            }

            

       

            

        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                }

                return new Response<Category?>(category);
            }
            catch
            {
                return new Response<Category?>(null, 500, "Erro ao buscar categoria");
            }


        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível alterar a categoria");
            }
        }

    }
}

//Comentários para meu controle pessoal
//Classe que funciona como Services e Repositories, aplica regra de negocios, e persiste dados no banco.
// é chamada pela HandleAsync em cada endpoint via injeção de dependencia.
