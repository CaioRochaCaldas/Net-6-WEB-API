﻿using APICatalogo.Context;

namespace APICatalogo.Repository
{
    public class UnityofWork : IUnitofWork
    {

        private ProdutoRepository _produtoRepo;
        private CategoriaRepository _categoriaRepo;
        public AppDbContext _context;

        public UnityofWork(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IProdutoRepository ProdutoRepository
        {
            get { return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context); }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get { return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context); }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
