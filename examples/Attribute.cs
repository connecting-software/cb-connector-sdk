public class Example
{
        //Define a property with ConnectorPropertyAttribute.
        [ConnectorProperty(
            Key = "Version",
            Name = "Version",
            Description = "The version of the API.",
            Flags = ConnectorPropertyFlags.None,
            DefaultValue = "v1.0",
            IsEncrypted = false)]
        public string Version { get; set; }
		
		//Define an OAuth property with ConnectorPropertyAttribute.
        [ConnectorProperty(
            Key = "ClientID",
            Name = "Client ID",
            Description = "The client identifier issued to the client during the application registration process.",
            Flags = (ConnectorPropertyFlags) ((int) ConnectorPropertyFlags.AuthMask &
                                              ((int) AuthenticationType.OAuth << 4)),
            Namespace = "IOAuth2Configuration",
            IsEncrypted = false)]
        public string ClientId { get; set; }
}
