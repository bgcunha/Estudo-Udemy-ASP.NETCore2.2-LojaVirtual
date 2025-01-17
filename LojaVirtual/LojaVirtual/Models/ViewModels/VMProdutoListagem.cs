﻿using LojaVirtual.Models.ProdutoAgregador;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Models.ViewModels
{
    public class VMProdutoListagem
    {
        public IPagedList<Produto> Lista { get; set; }
        public List<SelectListItem> Ordenacao
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Alfabética","A"),
                    new SelectListItem("Menor Preço","ME"),
                    new SelectListItem("Maior Preço","MA")
                };
            }
            private set { }
        }
    }
}
