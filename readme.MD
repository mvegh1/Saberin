Instructions to properly run this application!

1) Modify {CONNECTION_STRING} in the entityFramework/parameters/parameter node of Saberin.UI's web.config
	  <entityFramework>
		<defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework">
		  <parameters>
			<parameter value="{CONNECTION_STRING}" />
		  </parameters>
		</defaultConnectionFactory>
		<providers>
		  <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
		</providers>
	  </entityFramework>

2) Modify {USER_ID} in the appSettings/add[key="Saberin.Data.ExternalApi.USPS.UserId"] node of Saberin.UI's web.config to your USPS UserId 
3) Modify {PASSWORD} in the appSettings/add[key="Saberin.Data.ExternalApi.USPS.Password"] node of Saberin.UI's web.config to your USPS Password
    *** A note about the above. You must register access to USPS API at the following link: https://registration.shippingapis.com/
	*** Password is not required at this time
