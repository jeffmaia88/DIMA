using Dima.Api.Data;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Common.Extensions;

namespace Dima.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CreatedAt = DateTime.UtcNow,
                    Title = request.Title,
                    Type = request.Type,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,

                };

                await context.AddAsync(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar sua transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id);
                if (transaction == null)
                {
                    return new Response<Transaction?>(null, 404, " Transação não Encontrada");
                }

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação Excluida com Sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao Tentar Excluir Transação");
            }
            

           
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id);
                if(transaction == null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não Encontrada");

                }
                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Transação não Encontrada");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de ínicio ou término");
            }

            try
            {
                var query = context.Transactions.AsNoTracking().Where(t => t.PaidOrReceivedAt >= request.StartDate && t.PaidOrReceivedAt <= request.EndDate && t.UserId == request.UserId)
                                                               .OrderBy(t => t.PaidOrReceivedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions,count,request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
            }


        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId);
                if (transaction == null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não Encontrada");
                }

                transaction.Title = request.Title;
                transaction.Type =  request.Type;
                transaction.Amount = request.Amount;
                transaction.CategoryId = request.CategoryId;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação Alterada com Sucesso!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Erro ao buscar categoria");
            }
        }

        
    }
}
