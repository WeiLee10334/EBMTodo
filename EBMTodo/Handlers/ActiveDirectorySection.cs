using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace EBMTodo.Handlers
{
    public class ActiveDirectorySection : ConfigurationSection
    {
        [ConfigurationProperty(
          "ContainerControlID", IsRequired = false, DefaultValue = "ActiveDirectoryContainer")]
        public string ContainerControlID
        {
            get { return base["ContainerControlID"].ToString(); }
        }

        [ConfigurationProperty(
          "StateStoreTarget", IsRequired = false,
          DefaultValue = ActiveDirectoryContainerStateStoreEnum.Session)]
        public ActiveDirectoryContainerStateStoreEnum StateStoreTarget
        {
            get { return (ActiveDirectoryContainerStateStoreEnum)base["StateStoreTarget"]; }
        }

        [ConfigurationProperty(
          "UseActiveDirectory", IsRequired = true, DefaultValue = true)]
        public bool UseActiveDirectory
        {
            get { return Convert.ToBoolean(base["UseActiveDirectory"]); }
        }

        [ConfigurationProperty("ActiveDirectoryDomain", IsRequired = true)]
        public string ActiveDirectoryDomain
        {
            get { return base["ActiveDirectoryDomain"].ToString(); }
        }

        [ConfigurationProperty(
          "DefaultActiveDirectoryControlID", IsRequired = false,
          DefaultValue = "DefaultActiveDirectoryControl")]
        public string DefaultActiveDirectoryControlID
        {
            get { return base["DefaultActiveDirectoryControlID"].ToString(); }
        }

        [ConfigurationProperty(
         "ActiveDirectoryIP", IsRequired = true,
         DefaultValue = "ActiveDirectoryIP")]
        public string ActiveDirectoryIP
        {
            get { return base["ActiveDirectoryIP"].ToString(); }
        }
    }
}