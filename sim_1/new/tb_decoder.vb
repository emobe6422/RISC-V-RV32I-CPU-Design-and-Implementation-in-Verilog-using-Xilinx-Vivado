`timescale 1ns / 1ps

module tb_decoder();

    reg [31:0] instruction;
    wire [6:0] opcode;
    wire [4:0] write_register, rs1, rs2;
    wire [2:0] funct3;
    wire [6:0] funct7;
    
    decoder uut(
        .instruction(instruction),
        .opcode(opcode),
        .write_register(write_register),
        .rs1(rs1),
        .rs2(rs2),
        .funct3(funct3),
        .funct7(funct7)        
    );

    initial begin
        //sub x7, x2, x1
        instruction = 32'h401103b3; 
        #10;
        
        //add x3, x4, x5
        instruction = 32'h005201b3;
        #10;
        
        //and x4, x7, x9
        instruction = 32'h0093f233;
        #10;
        
        // End the simulation cleanly so it doesn't run forever
        $finish;
    end
    
endmodule