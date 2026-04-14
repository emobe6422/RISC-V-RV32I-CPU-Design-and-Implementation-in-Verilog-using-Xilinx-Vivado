`timescale 1ns / 1ps

module instruction_memory(
    input clk,
    input reset,
    input wire [31:0] instructionAddress,
    output reg [31:0] instruction
    );
    
    reg [31:0] instructionMem [0:1023];
    
endmodule
