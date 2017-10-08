// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.4.0.0
//    <NameSpace>Hybrasyl</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><DataMemberNameArg>OnlyIfDifferent</DataMemberNameArg><DataMemberOnXmlIgnore>False</DataMemberOnXmlIgnore><CodeBaseTag>Net20</CodeBaseTag><InitializeFields>AllExceptOptional</InitializeFields><GenerateUnusedComplexTypes>False</GenerateUnusedComplexTypes><GenerateUnusedSimpleTypes>False</GenerateUnusedSimpleTypes><GenerateXMLAttributes>True</GenerateXMLAttributes><OrderXMLAttrib>False</OrderXMLAttrib><EnableLazyLoading>False</EnableLazyLoading><VirtualProp>False</VirtualProp><PascalCase>False</PascalCase><AutomaticProperties>False</AutomaticProperties><PropNameSpecified>None</PropNameSpecified><PrivateFieldName>StartWithUnderscore</PrivateFieldName><PrivateFieldNamePrefix></PrivateFieldNamePrefix><EnableRestriction>False</EnableRestriction><RestrictionMaxLenght>False</RestrictionMaxLenght><RestrictionRegEx>False</RestrictionRegEx><RestrictionRange>False</RestrictionRange><ValidateProperty>False</ValidateProperty><ClassNamePrefix></ClassNamePrefix><ClassLevel>Public</ClassLevel><PartialClass>True</PartialClass><ClassesInSeparateFiles>False</ClassesInSeparateFiles><ClassesInSeparateFilesDir></ClassesInSeparateFilesDir><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><EnableAppInfoSettings>False</EnableAppInfoSettings><EnableExternalSchemasCache>False</EnableExternalSchemasCache><EnableDebug>False</EnableDebug><EnableWarn>False</EnableWarn><ExcludeImportedTypes>False</ExcludeImportedTypes><ExpandNesteadAttributeGroup>False</ExpandNesteadAttributeGroup><CleanupCode>False</CleanupCode><EnableXmlSerialization>False</EnableXmlSerialization><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><EnableEncoding>False</EnableEncoding><EnableXMLIndent>False</EnableXMLIndent><IndentChar>Indent1Space</IndentChar><NewLineAttr>False</NewLineAttr><OmitXML>False</OmitXML><Encoder>UTF8</Encoder><Serializer>XmlSerializer</Serializer><sspNullable>False</sspNullable><sspString>False</sspString><sspCollection>False</sspCollection><sspComplexType>False</sspComplexType><sspSimpleType>False</sspSimpleType><sspEnumType>False</sspEnumType><XmlSerializerEvent>False</XmlSerializerEvent><BaseClassName>EntityBase</BaseClassName><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><CustomUsings></CustomUsings><AttributesToExlude></AttributesToExlude>
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace Hybrasyl
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Xml;
    using System.Collections.Generic;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.hybrasyl.com/XML/Loot")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.hybrasyl.com/XML/Loot", IsNullable=false)]
    public partial class LootSet
    {
        
        #region Private fields
        private List<LootTable> _table;
        
        private string _name;
        #endregion
        
        public LootSet()
        {
            this._table = new List<LootTable>();
        }
        
        [System.Xml.Serialization.XmlElementAttribute("Table")]
        public List<LootTable> Table
        {
            get
            {
                return this._table;
            }
            set
            {
                this._table = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Loot")]
    public partial class LootTable
    {
        
        #region Private fields
        private LootTableItemList _items;
        
        private LootTableGold _gold;
        
        private int _rolls;
        
        private double _chance;
        #endregion
        
        public LootTable()
        {
            this._gold = new LootTableGold();
        }
        
        public LootTableItemList Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }
        
        public LootTableGold Gold
        {
            get
            {
                return this._gold;
            }
            set
            {
                this._gold = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Rolls
        {
            get
            {
                return this._rolls;
            }
            set
            {
                this._rolls = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Chance
        {
            get
            {
                return this._chance;
            }
            set
            {
                this._chance = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Loot")]
    public partial class LootTableItemList
    {
        
        #region Private fields
        private List<LootItem> _items;
        
        private int _rolls;
        
        private double _chance;
        #endregion
        
        public LootTableItemList()
        {
            this._items = new List<LootItem>();
        }
        
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable=false)]
        public List<LootItem> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Rolls
        {
            get
            {
                return this._rolls;
            }
            set
            {
                this._rolls = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Chance
        {
            get
            {
                return this._chance;
            }
            set
            {
                this._chance = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Loot")]
    public partial class LootItem
    {
        
        #region Private fields
        private int _min;
        
        private int _max;
        
        private bool _unique;
        
        private bool _always;
        
        private List<string> _variants;
        
        private string _value;
        #endregion
        
        public LootItem()
        {
            this._variants = new List<string>();
            this._min = 1;
            this._max = 1;
            this._unique = false;
            this._always = false;
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(1)]
        public int Min
        {
            get
            {
                return this._min;
            }
            set
            {
                this._min = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(1)]
        public int Max
        {
            get
            {
                return this._max;
            }
            set
            {
                this._max = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool Unique
        {
            get
            {
                return this._unique;
            }
            set
            {
                this._unique = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool Always
        {
            get
            {
                return this._always;
            }
            set
            {
                this._always = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public List<string> Variants
        {
            get
            {
                return this._variants;
            }
            set
            {
                this._variants = value;
            }
        }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.hybrasyl.com/XML/Loot")]
    public partial class LootTableGold
    {
        
        #region Private fields
        private int _min;
        
        private int _max;
        #endregion
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Min
        {
            get
            {
                return this._min;
            }
            set
            {
                this._min = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Max
        {
            get
            {
                return this._max;
            }
            set
            {
                this._max = value;
            }
        }
    }
}
#pragma warning restore