using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace Shengtai.Net
{
    public static partial class Extensions
    {
        public static string GetUserHostAddress(this HttpRequestBase request)
        {
            var userHostAddress = request.UserHostAddress;
            if (IPAddress.TryParse(userHostAddress, out IPAddress address))
            {
                var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(xForwardedFor))
                    return userHostAddress;

                // Get a list of public ip addresses in the X_FORWARDED_FOR variable
                var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPrivateIpAddress(ip)).ToList();

                // If we found any, return the last one, otherwise return the user host address
                return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
            }

            return "0.0.0.0";
        }

        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are:
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }

        public static string GetRemoteEndpointAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var operationContext = OperationContext.Current;
            if (operationContext == null)
                return ip;

            MessageProperties properties = operationContext.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (properties.Keys.Contains(HttpRequestMessageProperty.Name))
            {
                if (properties[HttpRequestMessageProperty.Name] is HttpRequestMessageProperty endpointLoadBalancer && endpointLoadBalancer.Headers["X-Forwarded-For"] != null)
                    ip = endpointLoadBalancer.Headers["X-Forwarded-For"];
            }

            if (string.IsNullOrEmpty(ip))
            {
                ip = endpoint.Address;
            }

            return ip;
        }
    }
}