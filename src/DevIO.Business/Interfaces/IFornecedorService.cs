using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task<bool> Adicionar(Fornecedor fornecedor);
        Task<bool> Atualizar(Fornecedor fornecedor);
        Task<bool> Remover(Guid Id);
        Task AtualizarEndereco(Endereco endereco);
    }
}
