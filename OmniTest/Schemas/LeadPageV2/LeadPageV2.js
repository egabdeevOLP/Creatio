define("LeadPageV2", [], function () {
	return {
		entitySchemaName: "Lead",
		attributes: {},
		modules: /**SCHEMA_MODULES*/ {} /**SCHEMA_MODULES*/,
		details: /**SCHEMA_DETAILS*/ {} /**SCHEMA_DETAILS*/,
		businessRules: /**SCHEMA_BUSINESS_RULES*/ {} /**SCHEMA_BUSINESS_RULES*/,
		methods: {
			init() {
				this.callParent(arguments);

				var esq = this.Ext.create("Terrasoft.EntitySchemaQuery", {
					rootSchemaName: "Lead"
				});
				esq.rowCount = 2;
				esq.addColumn("Id");
				esq.getEntityCollection(function (response) {
					if (response.success) {
						console.log("lead");
					}
				}, this);
			}
		},
		dataModels: /**SCHEMA_DATA_MODELS*/ {} /**SCHEMA_DATA_MODELS*/,
		diff: /**SCHEMA_DIFF*/ [
			{
				"operation": "insert",
				"name": "Cityedc4451f-89b5-4484-a99c-0eae6cb18ce6",
				"values": {
					"layout": {
						"colSpan": 12,
						"rowSpan": 1,
						"column": 0,
						"row": 4,
						"layoutName": "LeadPageRegisterInfoBlock"
					},
					"bindTo": "City"
				},
				"parentName": "LeadPageRegisterInfoBlock",
				"propertyName": "items",
				"index": 10
			},
			{
				"operation": "move",
				"name": "SalesOwnerV2",
				"parentName": "LeadPageSQLTabGridLayout",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "move",
				"name": "OpportunityDepartmentV2",
				"parentName": "LeadPageSQLTabGridLayout",
				"propertyName": "items",
				"index": 6
			}
		] /**SCHEMA_DIFF*/
	};
});
