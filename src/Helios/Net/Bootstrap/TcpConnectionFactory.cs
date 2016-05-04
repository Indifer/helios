﻿// Copyright (c) Petabridge <https://petabridge.com/>. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// See ThirdPartyNotices.txt for references to third party code used inside Helios.

using Helios.Net.Connections;
using Helios.Topology;

namespace Helios.Net.Bootstrap
{
    /// <summary>
    ///     <see cref="IConnectionFactory" /> for spawning <see cref="TcpConnection" /> instances
    /// </summary>
    public sealed class TcpConnectionFactory : ClientConnectionFactoryBase
    {
        public TcpConnectionFactory(ClientBootstrap clientBootstrap) : base(clientBootstrap)
        {
        }

        protected override IConnection CreateConnection(INode localEndpoint, INode remoteEndpoint)
        {
            return new TcpConnection(EventLoop, remoteEndpoint, Encoder, Decoder, Allocator);
        }
    }
}

