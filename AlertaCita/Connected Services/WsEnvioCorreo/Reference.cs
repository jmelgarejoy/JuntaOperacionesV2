﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlertaCita.WsEnvioCorreo {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MailPriority", Namespace="http://tempuri.org/")]
    public enum MailPriority : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Normal = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Low = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        High = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsEnvioCorreo.envioCorreoSoap")]
    public interface envioCorreoSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento CorreoRemitente del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EnvioCorreoInterno", ReplyAction="*")]
        AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponse EnvioCorreoInterno(AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EnvioCorreoInterno", ReplyAction="*")]
        System.Threading.Tasks.Task<AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponse> EnvioCorreoInternoAsync(AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento Asunto del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EnvioCorreoDepositoTemporal", ReplyAction="*")]
        AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponse EnvioCorreoDepositoTemporal(AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/EnvioCorreoDepositoTemporal", ReplyAction="*")]
        System.Threading.Tasks.Task<AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponse> EnvioCorreoDepositoTemporalAsync(AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EnvioCorreoInternoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EnvioCorreoInterno", Namespace="http://tempuri.org/", Order=0)]
        public AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequestBody Body;
        
        public EnvioCorreoInternoRequest() {
        }
        
        public EnvioCorreoInternoRequest(AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EnvioCorreoInternoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CorreoRemitente;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string NombreRemitente;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Asunto;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string CuerpoCorreo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public AlertaCita.WsEnvioCorreo.MailPriority Prioridad;
        
        public EnvioCorreoInternoRequestBody() {
        }
        
        public EnvioCorreoInternoRequestBody(string CorreoRemitente, string NombreRemitente, string Asunto, string CuerpoCorreo, AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios, AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia, AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta, AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos, AlertaCita.WsEnvioCorreo.MailPriority Prioridad) {
            this.CorreoRemitente = CorreoRemitente;
            this.NombreRemitente = NombreRemitente;
            this.Asunto = Asunto;
            this.CuerpoCorreo = CuerpoCorreo;
            this.ListaDestinatarios = ListaDestinatarios;
            this.ListaCopia = ListaCopia;
            this.ListaOculta = ListaOculta;
            this.ListaArchivos = ListaArchivos;
            this.Prioridad = Prioridad;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EnvioCorreoInternoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EnvioCorreoInternoResponse", Namespace="http://tempuri.org/", Order=0)]
        public AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponseBody Body;
        
        public EnvioCorreoInternoResponse() {
        }
        
        public EnvioCorreoInternoResponse(AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EnvioCorreoInternoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string EnvioCorreoInternoResult;
        
        public EnvioCorreoInternoResponseBody() {
        }
        
        public EnvioCorreoInternoResponseBody(string EnvioCorreoInternoResult) {
            this.EnvioCorreoInternoResult = EnvioCorreoInternoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EnvioCorreoDepositoTemporalRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EnvioCorreoDepositoTemporal", Namespace="http://tempuri.org/", Order=0)]
        public AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequestBody Body;
        
        public EnvioCorreoDepositoTemporalRequest() {
        }
        
        public EnvioCorreoDepositoTemporalRequest(AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EnvioCorreoDepositoTemporalRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string Asunto;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string CuerpoCorreo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public AlertaCita.WsEnvioCorreo.MailPriority Prioridad;
        
        public EnvioCorreoDepositoTemporalRequestBody() {
        }
        
        public EnvioCorreoDepositoTemporalRequestBody(string Asunto, string CuerpoCorreo, AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios, AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia, AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta, AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos, AlertaCita.WsEnvioCorreo.MailPriority Prioridad) {
            this.Asunto = Asunto;
            this.CuerpoCorreo = CuerpoCorreo;
            this.ListaDestinatarios = ListaDestinatarios;
            this.ListaCopia = ListaCopia;
            this.ListaOculta = ListaOculta;
            this.ListaArchivos = ListaArchivos;
            this.Prioridad = Prioridad;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EnvioCorreoDepositoTemporalResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EnvioCorreoDepositoTemporalResponse", Namespace="http://tempuri.org/", Order=0)]
        public AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponseBody Body;
        
        public EnvioCorreoDepositoTemporalResponse() {
        }
        
        public EnvioCorreoDepositoTemporalResponse(AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EnvioCorreoDepositoTemporalResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string EnvioCorreoDepositoTemporalResult;
        
        public EnvioCorreoDepositoTemporalResponseBody() {
        }
        
        public EnvioCorreoDepositoTemporalResponseBody(string EnvioCorreoDepositoTemporalResult) {
            this.EnvioCorreoDepositoTemporalResult = EnvioCorreoDepositoTemporalResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface envioCorreoSoapChannel : AlertaCita.WsEnvioCorreo.envioCorreoSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class envioCorreoSoapClient : System.ServiceModel.ClientBase<AlertaCita.WsEnvioCorreo.envioCorreoSoap>, AlertaCita.WsEnvioCorreo.envioCorreoSoap {
        
        public envioCorreoSoapClient() {
        }
        
        public envioCorreoSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public envioCorreoSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public envioCorreoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public envioCorreoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponse AlertaCita.WsEnvioCorreo.envioCorreoSoap.EnvioCorreoInterno(AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest request) {
            return base.Channel.EnvioCorreoInterno(request);
        }
        
        public string EnvioCorreoInterno(string CorreoRemitente, string NombreRemitente, string Asunto, string CuerpoCorreo, AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios, AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia, AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta, AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos, AlertaCita.WsEnvioCorreo.MailPriority Prioridad) {
            AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest inValue = new AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest();
            inValue.Body = new AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequestBody();
            inValue.Body.CorreoRemitente = CorreoRemitente;
            inValue.Body.NombreRemitente = NombreRemitente;
            inValue.Body.Asunto = Asunto;
            inValue.Body.CuerpoCorreo = CuerpoCorreo;
            inValue.Body.ListaDestinatarios = ListaDestinatarios;
            inValue.Body.ListaCopia = ListaCopia;
            inValue.Body.ListaOculta = ListaOculta;
            inValue.Body.ListaArchivos = ListaArchivos;
            inValue.Body.Prioridad = Prioridad;
            AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponse retVal = ((AlertaCita.WsEnvioCorreo.envioCorreoSoap)(this)).EnvioCorreoInterno(inValue);
            return retVal.Body.EnvioCorreoInternoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponse> AlertaCita.WsEnvioCorreo.envioCorreoSoap.EnvioCorreoInternoAsync(AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest request) {
            return base.Channel.EnvioCorreoInternoAsync(request);
        }
        
        public System.Threading.Tasks.Task<AlertaCita.WsEnvioCorreo.EnvioCorreoInternoResponse> EnvioCorreoInternoAsync(string CorreoRemitente, string NombreRemitente, string Asunto, string CuerpoCorreo, AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios, AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia, AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta, AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos, AlertaCita.WsEnvioCorreo.MailPriority Prioridad) {
            AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest inValue = new AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequest();
            inValue.Body = new AlertaCita.WsEnvioCorreo.EnvioCorreoInternoRequestBody();
            inValue.Body.CorreoRemitente = CorreoRemitente;
            inValue.Body.NombreRemitente = NombreRemitente;
            inValue.Body.Asunto = Asunto;
            inValue.Body.CuerpoCorreo = CuerpoCorreo;
            inValue.Body.ListaDestinatarios = ListaDestinatarios;
            inValue.Body.ListaCopia = ListaCopia;
            inValue.Body.ListaOculta = ListaOculta;
            inValue.Body.ListaArchivos = ListaArchivos;
            inValue.Body.Prioridad = Prioridad;
            return ((AlertaCita.WsEnvioCorreo.envioCorreoSoap)(this)).EnvioCorreoInternoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponse AlertaCita.WsEnvioCorreo.envioCorreoSoap.EnvioCorreoDepositoTemporal(AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest request) {
            return base.Channel.EnvioCorreoDepositoTemporal(request);
        }
        
        public string EnvioCorreoDepositoTemporal(string Asunto, string CuerpoCorreo, AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios, AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia, AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta, AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos, AlertaCita.WsEnvioCorreo.MailPriority Prioridad) {
            AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest inValue = new AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest();
            inValue.Body = new AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequestBody();
            inValue.Body.Asunto = Asunto;
            inValue.Body.CuerpoCorreo = CuerpoCorreo;
            inValue.Body.ListaDestinatarios = ListaDestinatarios;
            inValue.Body.ListaCopia = ListaCopia;
            inValue.Body.ListaOculta = ListaOculta;
            inValue.Body.ListaArchivos = ListaArchivos;
            inValue.Body.Prioridad = Prioridad;
            AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponse retVal = ((AlertaCita.WsEnvioCorreo.envioCorreoSoap)(this)).EnvioCorreoDepositoTemporal(inValue);
            return retVal.Body.EnvioCorreoDepositoTemporalResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponse> AlertaCita.WsEnvioCorreo.envioCorreoSoap.EnvioCorreoDepositoTemporalAsync(AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest request) {
            return base.Channel.EnvioCorreoDepositoTemporalAsync(request);
        }
        
        public System.Threading.Tasks.Task<AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalResponse> EnvioCorreoDepositoTemporalAsync(string Asunto, string CuerpoCorreo, AlertaCita.WsEnvioCorreo.ArrayOfString ListaDestinatarios, AlertaCita.WsEnvioCorreo.ArrayOfString ListaCopia, AlertaCita.WsEnvioCorreo.ArrayOfString ListaOculta, AlertaCita.WsEnvioCorreo.ArrayOfString ListaArchivos, AlertaCita.WsEnvioCorreo.MailPriority Prioridad) {
            AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest inValue = new AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequest();
            inValue.Body = new AlertaCita.WsEnvioCorreo.EnvioCorreoDepositoTemporalRequestBody();
            inValue.Body.Asunto = Asunto;
            inValue.Body.CuerpoCorreo = CuerpoCorreo;
            inValue.Body.ListaDestinatarios = ListaDestinatarios;
            inValue.Body.ListaCopia = ListaCopia;
            inValue.Body.ListaOculta = ListaOculta;
            inValue.Body.ListaArchivos = ListaArchivos;
            inValue.Body.Prioridad = Prioridad;
            return ((AlertaCita.WsEnvioCorreo.envioCorreoSoap)(this)).EnvioCorreoDepositoTemporalAsync(inValue);
        }
    }
}
