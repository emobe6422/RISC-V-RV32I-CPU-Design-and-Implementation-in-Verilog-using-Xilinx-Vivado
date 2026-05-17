`timescale 1ns / 1ps
module program_counter(
    input wire clk,
    input wire reset,
    input wire branch_mux_signal,
    input wire [31:0] B_immediate,
    input wire [31:0] PC,
    output reg [31:0] next_PC
);

    always @(*) begin
        if (branch_mux_signal)
            next_PC = PC + B_immediate;
        else
            next_PC = PC + 4;
    end

endmodule
