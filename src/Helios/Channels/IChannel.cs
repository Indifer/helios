﻿using System.Threading.Tasks;
using Helios.Net;
using Helios.Ops;
using Helios.Topology;

namespace Helios.Channels
{
    /// <summary>
    /// Composable wrapper interface over a <see cref="IConnection"/> - designed to enable
    /// buffering of messages, asynchronous operations, and dispatch available socket data to multiple workers
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// The unique ID for this channel
        /// </summary>
        IChannelId Id { get; }

        /// <summary>
        /// The eventloop responsible for executing commands on this channel
        /// </summary>
        IEventLoop EventLoop { get; }

        /// <summary>
        /// The pipeline of <see cref="IChannelHandler"/> instances responsible for handling this channel
        /// </summary>
        ChannelPipeline Pipeline { get; }

        /// <summary>
        /// Gets the parent <see cref="IChannel"/> responsible for this channel.
        /// 
        /// For instance, if this connection was created by an inbound TCP connnection, the Server channel
        /// responsible for creating this client connection is the Parent.
        /// </summary>
        IChannel Parent { get; }

        /// <summary>
        /// If the channel is open and might be <see cref="IsActive"/> later
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// If this channel has successfully received or sent any data
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Returns true if this channel is registered with an <see cref="IEventLoop"/>
        /// </summary>
        bool IsRegistered { get; }

        /// <summary>
        /// The local address that this channel is bound to
        /// </summary>
        INode LocalAddress { get; }

        /// <summary>
        /// The remote address that this channel is bound to
        /// </summary>
        INode RemoteAddress { get; }

        /// <summary>
        /// Returns the Task which will be completed once this channel is closed. This method
        /// always returns the same Task.
        /// </summary>
        Task<bool> CloseTask { get; }

        /// <summary>
        /// Returns true if the I/O thread will peform the requested write operation immediately.
        /// Any write requests made when this method returns false are queued until the I/O thread
        /// is ready to process the queued write requests.
        /// </summary>
        bool IsWriteable { get; }

        Task<bool> Bind(INode localAddress);

        Task<bool> Bind(INode localAddress, TaskCompletionSource<bool> bindCompletionSource);

        Task<bool> Connect(INode remoteAddress);

        Task<bool> Connect(INode remoteAddress, TaskCompletionSource<bool> connectCompletionSource);

        Task<bool> Connect(INode remoteAddress, INode localAddress);

        Task<bool> Connect(INode remoteAddress, INode localAddress, TaskCompletionSource<bool> connectCompletionSource);

        Task<bool> Disconnect();

        Task<bool> Disconnect(TaskCompletionSource<bool> disconnectCompletionSource);

        Task<bool> Close(TaskCompletionSource<bool> closeCompletionSource);

        IChannel Read();

        Task<bool> Write(NetworkData message);

        Task<bool> Write(NetworkData message, TaskCompletionSource<bool> writeCompletionSource);

        IChannel Flush();

        Task<bool> WriteAndFlush(NetworkData message, TaskCompletionSource<bool> writeCompletionSource);

        Task<bool> WriteAndFlush(NetworkData message);
    }
}