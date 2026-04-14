`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 03/14/2026 12:08:43 PM
// Design Name: 
// Module Name: control_unit
// Project Name: 
// Target Devices: 
// Tool Versions: 
// Description: 
// 
// Dependencies: 
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
//////////////////////////////////////////////////////////////////////////////////

//focusing on implementing ALUOp and ALUControl first
module control_unit(
    input wire [6:0] opcode,
    //funct7 and funct3
    input wire [2:0] funct3,
    input wire [6:0] funct7,
    //1-bit control signals
    output reg branch,
    output reg memRead,
    output reg memToReg,
    output reg memWrite,
    output reg ALUSrc,
    output reg regWrite,
    //2-bit control signal
    output reg [1:0] ALUOp,
    //4-bit signal derived w/ ALUOp, funct3, funct7
    output reg [3:0] ALUControl
    );
    
    always @(*) begin
        branch = 1'b0;
        memRead = 1'b0;
        memToReg = 1'b0;
        memWrite = 1'b0;
        ALUSrc = 1'b0;
        regWrite = 1'b0;
        ALUOp = 2'b00;
        ALUControl = 4'b0010;
        
        case (opcode)
        //ALUOp and ALUControl construction
            7'b1100011: begin //branch
                branch = 1'b1;
                ALUOp = 2'b01;
            end
            7'b0110011: begin //R type
                regWrite = 1'b1;
                ALUOp = 2'b10;
            end
            7'b0000011: begin //lw type (I)
                ALUSrc = 1'b1;
                regWrite = 1'b1;
                memToReg = 1'b1;
                memRead = 1'b1;
                ALUOp = 2'b00;
            end
            7'b0100011: begin //sw type (S)    
                ALUSrc = 1'b1;
                memWrite = 1'b1;
                ALUOp = 2'b00;
            end
        endcase
        //Construct ALUControl
        case (ALUOp) 
            2'b00: begin
                ALUControl = 4'b0010; //lw/sw
            end
            2'b01: begin
                ALUControl = 4'b0110; //beq
            end
            2'b10: begin
                //parse funct7 then go to funct3
                case (funct7)
                    7'b0100000: begin
                        ALUControl = 4'b0110; //sub 
                    end
                    7'b0000000: begin
                        //funct3 nested case
                        case (funct3)
                            3'b000: begin
                                ALUControl = 4'b0010; //add
                            end
                            3'b111: begin
                                ALUControl = 4'b0000; //and
                            end
                            3'b110: begin
                                ALUControl = 4'b0001; //or
                            end
                        endcase
                    end
                endcase
            end
        endcase
      end
endmodule
