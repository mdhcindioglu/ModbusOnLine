using Karonda.ModbusTcp;
using Karonda.ModbusTcp.Entity.Function.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modbus
{
    public class ModbusConnetion
    {
        readonly ModbusClient _client;
        public bool IsConnected { get; internal set; }
        public Exception Exception { get; set; }

        /// <summary>
        /// Connect to modbus device
        /// </summary>
        /// <param name="unitIdentifier">Device Number</param>
        /// <param name="ip">Device IP</param>
        /// <param name="port">Device Posrt</param>
        public ModbusConnetion(short unitIdentifier, string ip, int port = 502)
        {
            _client = new ModbusClient(unitIdentifier, ip, port);
        }


        /// <summary>
        /// Open the connection
        /// </summary>
        public async void Connect()
        {
            try
            {
                IsConnected = true;
                await _client.Connect();
            }
            catch(Exception ex) { Exception = ex; }
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public async void Close()
        {
            try
            {
                IsConnected = false;
                await _client.Close();
            }
            catch(Exception ex) { Exception = ex; }
        }

        /// <summary>
        /// Read Coils 0x01
        /// </summary>
        /// <param name="startingAddress">Starting Address</param>
        /// <param name="quantity">Address number to read</param>
        /// <returns>list of bool values in the device memory as BitArray</returns>
        public ReadCoilsResponse ReadCoils(ushort startingAddress, ushort quantity = 1)
        {
            try
            {
                return _client.ReadCoils(startingAddress, quantity);
            }
            catch(Exception ex) { Exception = ex; return null; }
        }

        /// <summary>
        /// Read Discrete Inputs 0x02
        /// </summary>
        /// <param name="startingAddress">Starting Address</param>
        /// <param name="quantity">Address number to read</param>
        /// <returns>list of bool values in the device memory as BitArray</returns>
        public ReadDiscreteInputsResponse ReadDiscreteInputs(ushort startingAddress, ushort quantity = 1)
        {
            try
            {
                return _client.ReadDiscreteInputs(startingAddress, quantity);
            }
            catch(Exception ex) { Exception = ex; return null; }
        }

        /// <summary>
        /// Read Holding Registers 0x03
        /// </summary>
        /// <param name="startingAddress">Starting Address</param>
        /// <param name="quantity">Address number to read</param>
        /// <returns>list of ushort values in the device memory as ushort[]</returns>
        public ReadHoldingRegistersResponse ReadHolding(ushort startingAddress, ushort quantity = 1)
        {
            try
            {
                return _client.ReadHoldingRegisters(startingAddress, quantity);
            }
            catch(Exception ex) { Exception = ex; return null; }
        }

        /// <summary>
        /// Read Input Registers 0x04
        /// </summary>
        /// <param name="startingAddress">Starting Address</param>
        /// <param name="quantity">Address number to read</param>
        /// <returns>list of ushort values in the device memory as ushort[]</returns>
        public ReadInputRegistersResponse ReadInputRegisters(ushort startingAddress, ushort quantity = 1)
        {
            try
            {
                return _client.ReadInputRegisters(startingAddress, quantity);
            }
            catch(Exception ex) { Exception = ex; return null; }
        }
    }
}
