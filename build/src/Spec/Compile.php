<?php
namespace Cornerstone\Quarry\Spec;
use Cornerstone\Quarry\Targets\RawForm;

class Compile
{
    function cli($args, $config)
    {
        if ($args) {
            $files = [$args[0]];
        } else {
            $files = glob("../spec/*.json");
        }

        foreach ($files as $fname) {
            $f = json_decode(file_get_contents($fname), 1);
            $name = pathinfo($fname)['filename'];
            $path = "work/$name." . RawForm::name . ".request";
            $out = RawForm::convert($f, $config);
            file_put_contents($path, $out);
        }
    }
}
