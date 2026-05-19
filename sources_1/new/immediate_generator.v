`timescale 1ns / 1ps

module immediate_generator(
    input wire [31:0] instruction,
    output reg [31:0] I_immediate,
    output reg [31:0] S_immediate,
    output reg [31:0] B_immediate,
    output reg [31:0] U_immediate,
    output reg [31:0] J_immediate
    );
    
    always @(*) begin
        I_immediate = { {20{instruction[31]}}, instruction[31:20] };
        S_immediate = { {20{instruction[31]}}, instruction[31:25], instruction[11:7] };
        B_immediate = { {20{instruction[31]}}, instruction[7], instruction[30:25], instruction[11:8], 1'b0};
        U_immediate = { instruction[31:12], 12'b0 };
        J_immediate = { {11{instruction[31]}}, instruction[31], instruction[19:12], instruction[20], instruction[30:21], 1'b0};
    end   
endmodule
