//
//  Simple.h
//  Simple
//
//  Created by Hiroaki Yamane on 5/1/18.
//  Copyright © 2018 Hiroaki Yamane. All rights reserved.
//

#ifndef Simple_
#define Simple_

/* The classes below are exported */
#pragma GCC visibility push(default)
#define ghAPI extern "C"

ghAPI int SimpleFn(int n) {
    return n * n;
}

#pragma GCC visibility pop
#endif
