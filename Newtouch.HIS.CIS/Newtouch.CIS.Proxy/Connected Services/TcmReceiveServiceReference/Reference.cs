﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Newtouch.CIS.Proxy.TcmReceiveServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.web.safesoft.com/", ConfigurationName="TcmReceiveServiceReference.ReceiveWebServiceInterface")]
    public interface ReceiveWebServiceInterface {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string TCM_HIS_08(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> TCM_HIS_08Async(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string TCM_HIS_07(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> TCM_HIS_07Async(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string TCM_HIS_09(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        System.Threading.Tasks.Task<string> TCM_HIS_09Async(string arg0);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ReceiveWebServiceInterfaceChannel : Newtouch.CIS.Proxy.TcmReceiveServiceReference.ReceiveWebServiceInterface, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReceiveWebServiceInterfaceClient : System.ServiceModel.ClientBase<Newtouch.CIS.Proxy.TcmReceiveServiceReference.ReceiveWebServiceInterface>, Newtouch.CIS.Proxy.TcmReceiveServiceReference.ReceiveWebServiceInterface {
        
        public ReceiveWebServiceInterfaceClient() {
        }
        
        public ReceiveWebServiceInterfaceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReceiveWebServiceInterfaceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReceiveWebServiceInterfaceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReceiveWebServiceInterfaceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string TCM_HIS_08(string arg0) {
            return base.Channel.TCM_HIS_08(arg0);
        }
        
        public System.Threading.Tasks.Task<string> TCM_HIS_08Async(string arg0) {
            return base.Channel.TCM_HIS_08Async(arg0);
        }
        
        public string TCM_HIS_07(string arg0) {
            return base.Channel.TCM_HIS_07(arg0);
        }
        
        public System.Threading.Tasks.Task<string> TCM_HIS_07Async(string arg0) {
            return base.Channel.TCM_HIS_07Async(arg0);
        }
        
        public string TCM_HIS_09(string arg0) {
            return base.Channel.TCM_HIS_09(arg0);
        }
        
        public System.Threading.Tasks.Task<string> TCM_HIS_09Async(string arg0) {
            return base.Channel.TCM_HIS_09Async(arg0);
        }
    }
}
