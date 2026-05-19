`timescale 1ns / 1ps

module tb_immediate_generator();

    reg [31:0] instruction;
    wire [31:0] I_immediate;
    wire [31:0] S_immediate;
    wire [31:0] B_immediate;
    wire [31:0] U_immediate;
    wire [31:0] J_immediate;
    
    immediate_generator uut(
        .instruction(instruction),
        .I_immediate(I_immediate),
        .S_immediate(S_immediate),
        .B_immediate(B_immediate),
        .U_immediate(U_immediate),
        .J_immediate(J_immediate)      
    );
    
    initial begin
        //addi x5, x6, -10 - I type
        instruction = 32'hffa30293;
        // want 32'hfffffffa 
        #10;
        
        //sw x7, 8(x8) - S type
        instruction = 32'h00742423;
        // want 32'h00000008       
        #10;
        
        //bne x9, x10, -20 - B type
        instruction = 32'hfeA496e3;
        // want 32'hffffffec       
        #10;
        
        //lui x11, 0x12345 - U type
        instruction = 32'h123455b7;
        // want 32'h12345000     
        #10;
        
        //jal x1, 2000 - J type
        instruction = 32'h7d0000ef;
        // want 32'h000007d0
        #10;

        $finish;
    end   
endmodule
