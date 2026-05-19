`timescale 1ns / 1ps

module program_counter(
    input wire branch_mux_signal,
    input wire jump,
    input wire jalr_sel,
    input wire [31:0] B_immediate,
    input wire [31:0] J_immediate,
    input wire [31:0] PC,
    input wire [31:0] sum,
    output reg [31:0] next_PC
);
    always @(*) begin
        if (jalr_sel)
            next_PC = sum;
        else if (jump)
            next_PC = PC + J_immediate;
        else if (branch_mux_signal)
            next_PC = PC + B_immediate;
        else
            next_PC = PC + 4;
    end
endmodule