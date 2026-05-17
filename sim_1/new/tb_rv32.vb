`timescale 1ns / 1ps
module tb_rv32;
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
        // ---- TEST GROUP 1: I-type arithmetic ----
        // addi x1, x0, 5
        // addi x2, x0, 3
        // xori x3, x1, 6
        // ori  x4, x1, 2
        // andi x5, x1, 6
        // slli x6, x1, 1
        // srli x7, x6, 1
        // srai x8, x1, 1
        repeat(10) @(posedge clk); #1;
        check(uut.register_file.registers[1],  32'h5, 1, "addi x1,x0,5");
        check(uut.register_file.registers[2],  32'h3, 2, "addi x2,x0,3");
        check(uut.register_file.registers[3],  32'h3, 3, "xori x3,x1,6");
        check(uut.register_file.registers[4],  32'h7, 4, "ori  x4,x1,2");
        check(uut.register_file.registers[5],  32'h4, 5, "andi x5,x1,6");
        check(uut.register_file.registers[6],  32'ha, 6, "slli x6,x1,1");
        check(uut.register_file.registers[7],  32'h5, 7, "srli x7,x6,1");
        check(uut.register_file.registers[8],  32'h2, 8, "srai x8,x1,1");

        // ---- TEST GROUP 2: R-type arithmetic ----
        // add  x9,  x1, x2
        // sub  x10, x1, x2
        // and  x11, x1, x2
        // or   x12, x1, x2
        // xor  x13, x1, x2
        // sll  x14, x1, x2
        // srl  x15, x14, x2
        // sra  x16, x1, x2
        repeat(8) @(posedge clk); #1;
        check(uut.register_file.registers[9],  32'h8,  9,  "add  x9,x1,x2");
        check(uut.register_file.registers[10], 32'h2,  10, "sub  x10,x1,x2");
        check(uut.register_file.registers[11], 32'h1,  11, "and  x11,x1,x2");
        check(uut.register_file.registers[12], 32'h7,  12, "or   x12,x1,x2");
        check(uut.register_file.registers[13], 32'h6,  13, "xor  x13,x1,x2");
        check(uut.register_file.registers[14], 32'h28, 14, "sll  x14,x1,x2");
        check(uut.register_file.registers[15], 32'h5,  15, "srl  x15,x14,x2");
        check(uut.register_file.registers[16], 32'h0,  16, "sra  x16,x1,x2");

        // ---- TEST GROUP 3: sw + lw ----
        // sw  x1,  0(x0)
        // lw  x17, 0(x0)
        repeat(3) @(posedge clk); #1;
        check(uut.register_file.registers[17], 32'h5, 17, "lw x17,0(x0)");

        // ---- TEST GROUP 4: branch ----
        // addi x18, x0, 5
        // addi x19, x0, 5
        // beq  x18, x19, 8
        // addi x20, x0, 99   (should be skipped)
        // addi x20, x0, 1
        repeat(6) @(posedge clk); #1;
        check(uut.register_file.registers[20], 32'h1, 18, "beq taken x20=1");

        $display("--------------------");
        $display("PASSED: %0d  FAILED: %0d", passed, failed);
        $finish;
    end

endmodule