`timescale 1ns / 1ps

module data_memory(
    input wire clk,
    //DO NOT USE RESET
    input wire memWrite,
    input wire memRead,
    input wire [31:0] rs2,
    input wire [31:0] address,
    output reg [31:0] addressData
    );

    reg [31:0] mem [0:1023];

    always @(posedge clk) begin
    
    //initialization of memory
    
        if (memWrite) begin
            mem[address[11:2]] <= rs2; // Use [11:2] to convert byte address to word index
        end
    end

    always @(*) begin
        addressData = 32'b0; //handles 00, 01, and 11 cases
        if (memRead && !memWrite) begin
            addressData = mem[address[11:2]];
        end
    end
endmodule