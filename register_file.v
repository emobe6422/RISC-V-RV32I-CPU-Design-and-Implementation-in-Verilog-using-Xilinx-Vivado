`timescale 1ns / 1ps

module register_file(
    input clk,
    input reset,
    //Enables the register to be written to when high 
    input wire regWrite,
    //registers
    input wire [4:0] rs1,
    input wire [4:0] rs2,
    input wire [4:0] rd,
    //data
    input wire [31:0] writeData,
    output wire [31:0] readData1,
    output wire [31:0] readData2
    );
    //create 32 x 32 bit registers. Think of as a black box we pass values in and out of
    reg [31:0] registers [0:31];
    integer i;
    
    //if rs1 = x0, assign 0, else assign whatever is in the register
    assign readData1 = (rs1 == 5'b00000) ? 32'b0 : registers[rs1];
    assign readData2 = (rs2 == 5'b00000) ? 32'b0 : registers[rs2];
    
    always @ (posedge clk) begin 
        if (reset) begin //figure out what thsi syntax actually means
            //Initialize the registers. They start in an undefined state otherwise
            for (i = 0; i < 32; i = i + 1) begin
                registers[i] <= 32'b0;
            end
        end
        //if we write to 0 ignore it even if regWrite is high
        else if(regWrite == 1'b1 && rd == 5'b00000) begin
            registers[rd] <= 5'b00000;
        end 
        else if (regWrite == 1'b1 && !(rd == 5'b00000)) begin
            registers[rd] <= writeData;
        end
        //regWrite is zero so throw it all out
    end  
endmodule
