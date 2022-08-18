using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using Terrasoft.Core;
using Terrasoft.Web.Common;
using Terrasoft.Core.Entities;
using Terrasoft.Core.DB;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using Common.Logging;
using Terrasoft.Common;

namespace Terrasoft.Configuration
{
    // ---------------------НАЙДИ МЕНЯ--------------------------------
    // ---------------------НАШЕЛ--------------------------------
    public class OmniTestService
    {
        private readonly UserConnection _userConnection;
        public OmniTestService(UserConnection userConnection)
        {
            _userConnection = userConnection;
        }

        public IEnumerable<Lead> GetAllLeads()
        {
            var select = new Select(_userConnection)
                .From("Lead")
                .Cols
                (
                    "Id",
                    "LeadName",
                    "CreatedOn"
                );

            return select.ExecuteEnumerable(l => new Lead(_userConnection)
            {
                Id = l.GetColumnValue<Guid>("Id"),
                LeadName = l.GetColumnValue<string>("LeadName"),
                CreatedOn = l.GetColumnValue<DateTime>("CreatedOn")
            });
        }

        public EntityCollection GetByName(string leadName)
        {
            
            var esq = new EntitySchemaQuery(_userConnection.EntitySchemaManager.GetInstanceByName("Lead"));
            esq.AddAllSchemaColumns();
            esq.Filters.Add(esq.CreateFilterWithParameters(FilterComparisonType.Contain, "LeadName", leadName));
            return esq.GetEntityCollection(_userConnection);
        }

        public Lead CreateLeadEntity(LeadDto leadDto)
        {
            Lead lead = new Lead(_userConnection);
            lead.SetDefColumnValues();
            lead.Contact = leadDto.Fullname;
            lead.MobilePhone = leadDto.Phone;
            lead.Account = leadDto.Organization;
            return lead;
        }
    }

    public class IntegrationLeadService
    {
        private readonly UserConnection _userConnection;
        public IntegrationLeadService(UserConnection userConnection)
        {
            _userConnection = userConnection;
        }

        public void CreateLead(LeadDto leadDto)
        {
            var lead = CreateLeadEntity(leadDto);
            lead.Save();

            var products = new EntityCollection(_userConnection, new LeadProduct(_userConnection).Schema);
            foreach (var productDto in leadDto.Products)
                products.Add(CreateProductEntity(productDto, lead.Id));
            products.Save();
        }

        public Lead CreateLeadEntity(LeadDto leadDto)
        {
            Lead lead = new Lead(_userConnection);
            lead.SetDefColumnValues();
            lead.Contact = leadDto.Fullname;
            lead.MobilePhone = leadDto.Phone;
            lead.Account = leadDto.Organization;
            return lead;
        }

        public LeadProduct CreateProductEntity(ProductDto productDto, Guid leadId)
        {
            LeadProduct product = new LeadProduct(_userConnection);
            product.ProductName = productDto.Name;
            product.LeadId = leadId;
            return product;
        }
    }

    public class LeadDto
    {
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Organization { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public List<ProductDto> Products { get; set; }
    }

    public class ProductDto
    {
        public string Name { get; set; }
    }
}