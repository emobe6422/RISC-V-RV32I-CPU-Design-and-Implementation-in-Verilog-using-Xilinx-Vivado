`timescale 1ns / 1ps

/* Meant for R type instructions. Immediate Generator handles any instructions that
 * have immediate values
 */
  
module decoder(
    input wire [31:0] instruction,
    output reg [6:0] opcode,
    output reg [4:0] write_register, rs1, rs2,
    output reg [2:0] funct3,
    output reg [6:0] funct7
    );
    
    always @(*) begin
        opcode = instruction[6:0];
        write_register = instruction[11:7];
        funct3 = instruction[14:12];        
        rs1 = instruction[19:15];
        rs2 = instruction[24:20];
        funct7 = instruction[31:25];
    end
endmodule
