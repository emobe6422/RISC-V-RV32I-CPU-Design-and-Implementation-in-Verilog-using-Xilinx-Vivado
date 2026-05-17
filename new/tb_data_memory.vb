`timescale 1ns / 1ps
module tb_data_memory;

    reg clk;
    reg mem_read, mem_write;
    reg [31:0] address;
    reg [31:0] ram_write_data;
    reg [3:0] write_mask;
    wire [31:0] read_data;

    data_memory uut(
        .clk(clk),
        .mem_read(mem_read),
        .mem_write(mem_write),
        .address(address),
        .ram_write_data(ram_write_data),
        .write_mask(write_mask),
        .read_data(read_data)
    );

    always #5 clk = ~clk;

    task check;
        input [31:0] got;
        input [31:0] expected;
        input [63:0] test_num;
        begin
            if (got === expected)
                $display("TEST %0d PASS: got %h", test_num, got);
            else
                $display("TEST %0d FAIL: got %h, expected %h", test_num, got, expected);
        end
    endtask

    initial begin
        clk = 0;
        mem_read = 0;
        mem_write = 0;
        address = 0;
        ram_write_data = 0;
        write_mask = 4'b0000;
        #10;

        // ---- TEST 1: basic write then read ----
        mem_write = 1;
        mem_read = 0;
        address = 32'h0;
        ram_write_data = 32'hDEADBEEF;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        mem_write = 0;
        mem_read = 1;
        @(posedge clk); #1;
        check(read_data, 32'hDEADBEEF, 1);

        // ---- TEST 2: write mask only bottom byte ----
        // prefill address 4 with all 1s
        mem_write = 1;
        mem_read = 0;
        address = 32'h4;
        ram_write_data = 32'hFFFFFFFF;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        // now overwrite only bottom byte with 0xAB
        ram_write_data = 32'h000000AB;
        write_mask = 4'b0001;
        @(posedge clk); #1;

        mem_write = 0;
        mem_read = 1;
        @(posedge clk); #1;
        check(read_data, 32'hFFFFFFAB, 2);

        // ---- TEST 3: different addresses dont bleed ----
        mem_write = 1;
        mem_read = 0;
        address = 32'h8;
        ram_write_data = 32'hAAAAAAAA;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        address = 32'hC;
        ram_write_data = 32'h55555555;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        mem_write = 0;
        mem_read = 1;
        address = 32'h8;
        @(posedge clk); #1;
        check(read_data, 32'hAAAAAAAA, 3);

        address = 32'hC;
        @(posedge clk); #1;
        check(read_data, 32'h55555555, 4);

        // ---- TEST 4: mem_write=0 does not write ----
        mem_write = 0;
        mem_read = 0;
        address = 32'h10;
        ram_write_data = 32'h12345678;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        mem_read = 1;
        @(posedge clk); #1;
        // memory was never written so should be 0 (uninitialized)
        // just verify it is NOT the value we tried to write
        if (read_data !== 32'h12345678)
            $display("TEST 5 PASS: write was correctly blocked");
        else
            $display("TEST 5 FAIL: write went through without mem_write");

        // ---- TEST 5: boundary - address 0 ----
        mem_write = 1;
        mem_read = 0;
        address = 32'h0;
        ram_write_data = 32'hCAFEBABE;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        mem_write = 0;
        mem_read = 1;
        @(posedge clk); #1;
        check(read_data, 32'hCAFEBABE, 6);

        // ---- TEST 6: boundary - address 1023 (word index) = byte address hFFC ----
        mem_write = 1;
        mem_read = 0;
        address = 32'hFFC;
        ram_write_data = 32'h0BADF00D;
        write_mask = 4'b1111;
        @(posedge clk); #1;

        mem_write = 0;
        mem_read = 1;
        @(posedge clk); #1;
        check(read_data, 32'h0BADF00D, 7);

        $display("done");
        $finish;
    end

endmodule