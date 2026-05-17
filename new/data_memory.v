`timescale 1ns / 1ps

module data_memory(
    input clk,
    input wire mem_read, mem_write,
    input wire [31:0] address, // memory address
    input wire [31:0] ram_write_data,
    input wire [3:0] write_mask,
    output reg [31:0] read_data
    );
    
    reg [31:0] memory [0:1023]; //2^10 -> 1024 and divide by 4 e.g. remove 2 bits
    
    initial begin
        $readmemh("C:/Users/Emman/riscv_32i/data.hex", memory);
    end
    
    always @(posedge clk) begin
        if (mem_write) begin
            if (write_mask[0]) memory[address[11:2]][7:0]   <= ram_write_data[7:0];
            if (write_mask[1]) memory[address[11:2]][15:8]  <= ram_write_data[15:8];
            if (write_mask[2]) memory[address[11:2]][23:16] <= ram_write_data[23:16];
            if (write_mask[3]) memory[address[11:2]][31:24] <= ram_write_data[31:24];
        end
    end

    always @(*) begin
        if (mem_read && !mem_write)
            read_data = memory[address[11:2]];
        else
            read_data = 32'b0;
    end
endmodule