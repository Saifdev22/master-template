using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Application.Endpoints.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid Id)
        : IQuery<GetCategoryByIdResult>;

    public record GetCategoryByIdResult(Category category);
}
