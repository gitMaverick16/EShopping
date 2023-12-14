﻿using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.MapperExt.Map<Product>(request);
            if(productEntity is null)
            {
                throw new ApplicationException("There is an issue with mapping qhile creating new product");
            }
            var newProduct = await _productRepository.CreateProduct(productEntity);
            var productResponse = ProductMapper.MapperExt.Map<ProductResponse>(newProduct);
            return productResponse;
        }
    }
}
