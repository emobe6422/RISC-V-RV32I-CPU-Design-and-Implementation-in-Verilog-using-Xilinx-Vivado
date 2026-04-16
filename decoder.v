`timescale 1ns / 1ps

//Only R and I formats rn

 module decoder(
//start with R and I type. Later S and U
    input wire [31:0] instruction,
    output reg [6:0] opcode,
    //register destination
    output reg [4:0] rd,
    //extra parameters for ALU operation (AND, OR, add, sub, etc.)
    output reg [2:0] funct3,
    output reg [6:0] funct7,
    //register sources #1 and #2
    output reg [4:0] rs1,
    output reg [4:0] rs2,
    //We don't need an immediate for R type since no immediates
    output reg [31:0] Iimm, //sign extended Immediate opcode
    output reg [31:0] Simm,
    output reg [31:0] Bimm
    //output reg [31:0] Jimm
    );
    
    always @(*) begin 
        //get opcode
        opcode = instruction[6:0];
        //get register destination
        rd  = instruction[11:7];
        //Get funct3 and funct7
        funct3 = instruction[14:12];
        funct7 = instruction[31:25]; 
        //get Source Resigters.
        rs1 = instruction[19:15];
        rs2 = instruction[24:20];
        //assign immediates (signed sign extension)
        Iimm = {{21{instruction[31]}},instruction[30:20]}; 
        Simm = {{21{instruction[31]}},instruction[30:25],instruction[11:7]};
        Bimm = {{20{instruction[31]}},instruction[7],instruction[30:25],instruction[11:8],1'b0};
    end
endmodule
