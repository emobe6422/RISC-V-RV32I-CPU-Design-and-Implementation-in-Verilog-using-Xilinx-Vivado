`timescale 1ns / 1ps

module ALU(
    /*Since the 1st value passed into the ALU is always the same
    (doesn't need a mux to determine anything), its naming 
    convention is different*/
    input wire [31:0] readData1,
    input wire [31:0] inputTwo,
    input wire [3:0] ALUControl,
    output reg [31:0] sum,
    output wire zeroFlag
    );
    
    assign zeroFlag = (readData1 - inputTwo == 32'b0) ? 1'b1 : 1'b0;
    
    always @(*) begin
        case (ALUControl)
            4'b0000 : begin //AND
                sum = readData1 & inputTwo;
            end
            4'b0110 : begin //SUB/BEQ
                sum = readData1 - inputTwo;
            end
            4'b0010 : begin //LW/SW/ADD
                sum = readData1 + inputTwo;
            end
            4'b0001 : begin //OR
                sum = readData1 | inputTwo;
            end   
        endcase
    end
endmodule
