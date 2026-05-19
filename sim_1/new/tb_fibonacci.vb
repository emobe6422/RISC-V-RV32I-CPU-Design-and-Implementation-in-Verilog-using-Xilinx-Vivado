`timescale 1ns / 1ps
module tb_fibonacci;
    reg clk;
    integer failed = 0;
    integer passed = 0;
    
    rv32 uut (
        .clk(clk)
    );
    
    always #5 clk = ~clk;

    task check;
        input [31:0] got;
        input [31:0] expected;
        input [63:0] test_num;
        input [127:0] name;
        begin
            if (got === expected) begin
                $display("TEST %0d PASS (%s): got %h", test_num, name, got);
                passed = passed + 1;
            end else begin
                $display("TEST %0d FAIL (%s): got %h, expected %h", test_num, name, got, expected);
                failed = failed + 1;
            end
        end
    endtask

    initial begin
        clk = 0;
    end

    initial begin
        @(posedge clk); #1;
        repeat(20) begin
            @(posedge clk); #1;
            if (uut.instruction == 32'hfe021463) begin
                $display("BRANCH DEBUG: instruction=%h [31]=%b [7]=%b [30:25]=%b [11:8]=%b B_immediate=%h next_PC=%h PC=%h",
                    uut.instruction,
                    uut.instruction[31],
                    uut.instruction[7],
                    uut.instruction[30:25],
                    uut.instruction[11:8],
                    uut.B_immediate,
                    uut.next_PC,
                    uut.PC);
            end
        end
    end

    initial begin
        repeat(200) @(posedge clk); #1;
        check(uut.data_memory.memory[0], 32'h0,  1,  "fib(0)=0");
        check(uut.data_memory.memory[1], 32'h1,  2,  "fib(1)=1");
        check(uut.data_memory.memory[2], 32'h1,  3,  "fib(2)=1");
        check(uut.data_memory.memory[3], 32'h2,  4,  "fib(3)=2");
        check(uut.data_memory.memory[4], 32'h3,  5,  "fib(4)=3");
        check(uut.data_memory.memory[5], 32'h5,  6,  "fib(5)=5");
        check(uut.data_memory.memory[6], 32'h8,  7,  "fib(6)=8");
        check(uut.data_memory.memory[7], 32'hd,  8,  "fib(7)=13");
        check(uut.data_memory.memory[8], 32'h15, 9,  "fib(8)=21");
        check(uut.data_memory.memory[9], 32'h22, 10, "fib(9)=34");
        $display("--------------------");
        $display("PASSED: %0d  FAILED: %0d", passed, failed);
        $finish;
    end

endmodule