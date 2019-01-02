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
        //The flag will inform how to treat this property.
        //The namespace should be used for grouping properties.
        [ConnectorProperty(
            Key = "ClientID",
            Name = "Client ID",
            Description = "The client identifier.",
            Flags = (ConnectorPropertyFlags) ((int) ConnectorPropertyFlags.AuthMask &
                    ((int) AuthenticationType.OAuth2 << 4)),
            Namespace = "IOAuth2Configuration",
            IsEncrypted = false)]
        public string ClientId { get; set; }

        //Define an OAuth property with ConnectorPropertyAttribute.
        //Shadows represents a dynamic object stored in CB Server.
        [ConnectorProperty(
            Key = "ExpiresAt",
            Name = "ExpiresAt",
            Description = "The token's expiration date.",
            Flags = ConnectorPropertyFlags.Hidden | ConnectorPropertyFlags.ConnectorCanSet,
            Namespace = "IOAuth2Configuration",
            IsEncrypted = true)]
        public DateTime ExpiresAt
        {
            get => Shadows.ExpiresAt;
            set => Shadows.ExpiresAt = value;
        }
    }
